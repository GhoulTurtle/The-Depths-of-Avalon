//Last Editor: Caleb Richardson
//Last Edited: Feb 16
using System;
using UnityEngine;

public class SlidingBlock : MonoBehaviour{
    [Header("Sliding Block Variables")]
    [SerializeField, Range(1f, 10f)] private float slidingSpeed = 5f;
    [SerializeField, Range(-0.1f, -2f)] private float gravity = -15f;
    [SerializeField, Range(0.1f, 0.5f)] private float collisonDetectionRange = 0.1f;
    [SerializeField, Range(0.1f, 0.5f)] private float groundDetectionRange = 0.1f;

    [Header("LayerMask References")]
    [Tooltip("The layers that the block will be stopped by when sliding.")]
    [SerializeField] private LayerMask validStopLayerMask;
    [Tooltip("The layers that the block will push when sliding.")]
    [SerializeField] private LayerMask validPushLayerMask;
    [Tooltip("The layers that the block will recognize as ground.")]
    [SerializeField] private LayerMask groundLayerMask;

    [Header("Reset References")]
    [SerializeField] private Transform resetPoint;
    [SerializeField] private PlateObject resetPressurePlate;

    [Header("Debug Variables")]
    [SerializeField] private Color gizmosColor = Color.green;
    [SerializeField] private bool drawGizmos;

    private HealthSystem healthSystem;

    private bool isSliding = false;

    private Vector3 currentSlideDir;
    private Vector3 gravityVector = new Vector3();
    private float verticalVelocity;

    private void Awake() {
        TryGetComponent(out healthSystem);
    }

    private void Start() {
        healthSystem.OnDamaged += AttemptSlide;
        healthSystem.OnDie += ResetBlock;
        
        if(resetPressurePlate != null){
            resetPressurePlate.OnActivate += ResetBlock;
        }
    }

    private void OnDestroy(){
        healthSystem.OnDamaged -= AttemptSlide;
        healthSystem.OnDie -= ResetBlock;

        if(resetPressurePlate != null){
            resetPressurePlate.OnActivate -= ResetBlock;
        }
    }

    private void Update(){
        SlidingUpdate();
        Gravity();
    }

    private void AttemptSlide(object sender, HealthSystem.DamagedEventArgs e){
        if(isSliding) return;

        // Calculate the direction vector opposite to the damage source
        currentSlideDir = transform.position - e.damageSource.position;
        currentSlideDir.y = 0f; // Keep the movement in the horizontal plane
        currentSlideDir.Normalize(); // Normalize the vector to have a length of 1

        // Restrict the slideDirection to only forward, backward, left, and right
        float xComponent = Mathf.Abs(currentSlideDir.x) > Mathf.Abs(currentSlideDir.z) ? Mathf.Sign(currentSlideDir.x) : 0f;
        float zComponent = Mathf.Abs(currentSlideDir.z) > Mathf.Abs(currentSlideDir.x) ? Mathf.Sign(currentSlideDir.z) : 0f;

        currentSlideDir = new Vector3(xComponent, 0f, zComponent);
        isSliding = true;
    }

    private void ResetBlock(object sender, EventArgs e){
        isSliding = false;
        transform.position = resetPoint.position;
    }

    private void Gravity(){
        if(Physics.BoxCast(transform.position, transform.localScale / 2, Vector3.down, out RaycastHit hit, transform.rotation, groundDetectionRange, groundLayerMask)){
            verticalVelocity = 0;
        }
        else{
            verticalVelocity += gravity * Time.deltaTime;
            transform.position = CalculateGravityVector();
        }
    }

    private void SlidingUpdate(){
        if(!isSliding) return;
        if(Physics.BoxCast(transform.position,  transform.localScale / 2, currentSlideDir, transform.rotation, collisonDetectionRange, validStopLayerMask)){
            // Stop sliding when hitting an object
            isSliding = false;
        }
        else{
            SlideBlock();
        }

        if(Physics.BoxCast(transform.position,  transform.localScale / 2, currentSlideDir, out RaycastHit hit, transform.rotation, collisonDetectionRange, validPushLayerMask)){
            //add force to the pushable object
        }
    }

    private void SlideBlock(){
        transform.position = transform.position + currentSlideDir * (slidingSpeed * Time.deltaTime); 
    }

    private Vector3 CalculateGravityVector(){
        gravityVector.Set(transform.position.x, transform.position.y + verticalVelocity, transform.position.z);
        return gravityVector;
    }

    private void OnDrawGizmos(){
        if(!drawGizmos) return;

        Gizmos.color = gizmosColor;
        
        if(resetPoint != null){
            Gizmos.DrawWireCube(resetPoint.position, transform.localScale);
        }
    }
}
