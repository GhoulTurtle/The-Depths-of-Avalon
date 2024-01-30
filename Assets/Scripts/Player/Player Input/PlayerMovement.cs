using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour{
    [Header("Speed Variables")]
    [Range(3, 20)]
    [SerializeField] private float movementSpeed = 5f;
    [Range(3, 10)]
    [SerializeField] private float accelerationSpeed = 10f;
    [Range(3, 30)]
    [SerializeField] private float rotationSpeed;

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

    private Camera mainCamera;

    private Character playerCharacter;

    private void Awake() {
        TryGetComponent(out characterController);
        TryGetComponent(out playerCharacter);
        playerCharacter.OnSetupCharacter += UpdateMovementVariables;
    }

    private void OnDestroy() {
        playerCharacter.OnSetupCharacter -= UpdateMovementVariables;
    }

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        GroundCheck();
        Gravity();
        Move();
    }
    
    public void SetInputVector(Vector2 movement) => currentMovementDirection = movement;

    public void UpdatePosition(Vector3 pos){
        characterController.enabled = false;
        transform.position = pos;
        characterController.enabled = true;
    }

    private void UpdateMovementVariables(object sender, Character.SetupCharacterEventArgs e){
        movementSpeed = e.CharacterStats.movementSpeed;
        accelerationSpeed = e.CharacterStats.accelerationSpeed;
        rotationSpeed = e.CharacterStats.rotationSpeed;
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
            
            inputDirection = new Vector3(input.x, 0, input.y);
        }
        else{
            currentMovementDirection = Vector2.zero;
        }

        var targetVector = Quaternion.Euler(0, mainCamera.transform.eulerAngles.y, 0) * inputDirection;

        //Add vertical velocity to the calculation
        characterController.Move(targetVector * (currentSpeed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
        UpdateRotation(targetVector);
    }

    private void UpdateRotation(Vector3 movementDirection){
        if(movementDirection.magnitude == 0) { return; }

        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }
}
