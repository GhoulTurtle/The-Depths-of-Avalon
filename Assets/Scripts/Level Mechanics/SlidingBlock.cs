//Last Editor: Caleb Richardson
//Last Edited: Feb 15
using UnityEngine;

[RequireComponent(typeof(HealthSystem), typeof(Rigidbody))]
public class SlidingBlock : MonoBehaviour{
    [Header("Block Movement")]
    [SerializeField] private float slideSpeed = 5f;
    [SerializeField] private float collisonDetectionRange = 0.1f;
    [SerializeField] private float groundDetectionRange = 0.5f;

    [Header("Block Gravity")]
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float gravity = -80f;

    [Header("Required References")]
    public LayerMask validHitLayerMask; // When a moving box comes into contact with anything on this layer, it ignores it
    
    private HealthSystem healthSystem => GetComponent<HealthSystem>();
    private Rigidbody rb;
    
    private Vector3 slideDirection; // Added variable to store the slide direction
    private const float terminalVelocity = 53f;
    private float verticalVelocity;
    
    private bool isSliding = false;
    private bool isGrounded = false;

    private void Start() {
        healthSystem.OnDamaged += BeginSlide;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    private void OnDestroy() {
        healthSystem.OnDamaged -= BeginSlide;
    }

    private void Update() {
        if (isSliding){
            // Move the block using physics to handle collisions
            rb.AddForce(slideDirection * (slideSpeed * Time.deltaTime), ForceMode.VelocityChange);
            DetectCollison();
        }

        GroundCheck();
        Gravity();
    }

    private void BeginSlide(object sender, HealthSystem.DamagedEventArgs e) {
        if(isSliding) return;
        
        rb.isKinematic = false;

        // Calculate the direction vector opposite to the damage source
        slideDirection = transform.position - e.damageSource.position;
        slideDirection.y = 0f; // Keep the movement in the horizontal plane
        slideDirection.Normalize(); // Normalize the vector to have a length of 1

        // Restrict the slideDirection to only forward, backward, left, and right
        float xComponent = Mathf.Abs(slideDirection.x) > Mathf.Abs(slideDirection.z) ? Mathf.Sign(slideDirection.x) : 0f;
        float zComponent = Mathf.Abs(slideDirection.z) > Mathf.Abs(slideDirection.x) ? Mathf.Sign(slideDirection.z) : 0f;

        slideDirection = new Vector3(xComponent, 0f, zComponent);

        isSliding = true;
    }

    private void Gravity(){
        if(isGrounded){
            verticalVelocity = 0;
        }
        else if(verticalVelocity < terminalVelocity){
            verticalVelocity += gravity * Time.deltaTime;
        }

        if(isSliding){
            rb.velocity += new Vector3(0, verticalVelocity, 0);
        }
    }

    private void GroundCheck(){
        isGrounded = Physics.Raycast(transform.position, -transform.up, groundDetectionRange, groundLayers);
    }

    private void DetectCollison(){
        if(Physics.BoxCast(transform.position,  transform.localScale / 2, slideDirection, transform.rotation, collisonDetectionRange, validHitLayerMask)){
            // Stop sliding when hitting an object
            isSliding = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(transform.position, -transform.up * groundDetectionRange);
    }
}
