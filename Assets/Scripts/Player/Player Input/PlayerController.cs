using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour{
    [Header("Speed Variables")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float accelerationSpeed = 10f;

    [Header("Ground Check Variables")]
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private LayerMask groundLayers;

    [Header("Gravity")]
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float fallTimeout = 0.15f;
    
    private Vector2 currentMovementDirection;
    private CharacterController characterController;

    private const float terminalVelocity = 53f;
    private float currentSpeed;
    private float verticalVelocity;
    private float fallTimeoutDelta;

    private bool grounded = false;

    private void Awake() {
        TryGetComponent(out characterController);
    }

    private void Update() {
        GroundCheck();
        Gravity();
        Move();
    }

    private void Gravity(){
        if(grounded){
                fallTimeoutDelta = fallTimeout;

                // Stops our velocity dropping infitely when grounded
                if(verticalVelocity < 0f){
                    verticalVelocity = -2f;
                }
            }
            else{
                if(fallTimeoutDelta >= 0.0f){
                    fallTimeoutDelta -= Time.deltaTime;
                }
            }

        if(verticalVelocity < terminalVelocity){
            verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void GroundCheck(){
        grounded = Physics.CheckSphere(GetGroundCheckPosition(), groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    private Vector3 GetGroundCheckPosition(){
        return new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
    }

    public void OnMove(InputAction.CallbackContext context) => currentMovementDirection = context.ReadValue<Vector2>();
    
    private void Move(){
        float targetSpeed = movementSpeed;

        var input = currentMovementDirection;

        if(input == Vector2.zero) targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z).magnitude;
        float speedOffset = 0.1f;

        if(currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset){
            currentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * input.magnitude, Time.deltaTime * accelerationSpeed);

            currentSpeed = Mathf.Round(currentSpeed * 1000f) / 1000f;
        }

        else{
            currentSpeed = targetSpeed;
        }

        var normalizedMovementDirection = new Vector3(currentMovementDirection.x, 0f, currentMovementDirection.y).normalized;

        
        Vector3 inputDirection = normalizedMovementDirection;

        if(input != Vector2.zero){
            if(input != currentMovementDirection){
                currentMovementDirection = input;
            }
            
            inputDirection = transform.right * -input.y + transform.forward * input.x;
        }
        else{
            currentMovementDirection = Vector2.zero;
        }

        //Add vertical velocity to the calculation
        characterController.Move(inputDirection * (currentSpeed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }
}
