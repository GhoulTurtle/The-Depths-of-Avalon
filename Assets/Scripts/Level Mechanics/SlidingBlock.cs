using UnityEngine;

[RequireComponent(typeof(HealthSystem), typeof(Rigidbody))]
public class SlidingBlock : MonoBehaviour
{
    private HealthSystem healthSystem => GetComponent<HealthSystem>();
    private bool isSliding = false;
    private Rigidbody rb;
    private Vector3 slideDirection; // Added variable to store the slide direction

    public LayerMask ignoreHitLayer; // When a moving box comes into contact with anything on this layer, it ignores it
    public float slideSpeed = 5f;

    private void OnEnable() {
        healthSystem.OnDamaged += BeginSlide;
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable() {
        healthSystem.OnDamaged -= BeginSlide;
    }

    private void Update() {
        if (isSliding)
        {
            // Move the block using physics to handle collisions
            rb.velocity = slideDirection * slideSpeed;
        }
    }


    private void BeginSlide(object sender, HealthSystem.DamagedEventArgs e) {
        Debug.Log("Begin Sliding");

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

    private void OnCollisionEnter(Collision collision) {
        if((ignoreHitLayer.value & 1 << collision.gameObject.layer) != 0) {
            return;
        }

        // Check if the block has collided with an object
        if (isSliding)
        {
            // Stop sliding when hitting an object
            isSliding = false;
            rb.velocity = Vector3.zero;
        }

    }

}
