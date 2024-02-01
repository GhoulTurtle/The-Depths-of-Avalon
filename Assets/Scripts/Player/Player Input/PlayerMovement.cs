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
    
    private Vector2 currentInput;
    private CharacterController characterController;

    //Movement
    private Vector3 inputDirection;

    //Gravity
    private const float terminalVelocity = 53f;
    private float currentSpeed;
    private float verticalVelocity;

    //Checks
    private bool grounded = false;

    //References
    private CameraController mainCameraController;
    private Character playerCharacter;

    private void Awake() {
        TryGetComponent(out characterController);
        TryGetComponent(out playerCharacter);
        playerCharacter.OnSetupCharacter += UpdateMovementVariables;
        playerCharacter.OnStatusEffectApplied += StatusEffectApplied;
    }

    private void OnDestroy() {
        playerCharacter.OnSetupCharacter -= UpdateMovementVariables;
    }

    private void Start() {
        Camera.main.TryGetComponent(out mainCameraController);
    }

    private void Update() {
        GroundCheck();
        Gravity();
        Move();
    }
    
    public void SetInputVector(Vector2 _currentInput) => currentInput = _currentInput;

    public void UpdatePosition(Vector3 pos){
        characterController.enabled = false;
        transform.position = pos;
        characterController.enabled = true;
    }
    
    private void StatusEffectApplied(object sender, Character.StatusEffectAppliedEventArgs e){
        
    }

    private void UpdateMovementVariables(object sender, Character.SetupCharacterEventArgs e){
        movementSpeed = e.CharacterStats.movementSpeed;
        accelerationSpeed = e.CharacterStats.accelerationSpeed;
        rotationSpeed = e.CharacterStats.rotationSpeed;
    }

    private void Gravity(){
        if(grounded){
            verticalVelocity = -2f;
            return;
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
        //Project the input to see if that would set us to far from the camera midpoint
        inputDirection = new Vector3(currentInput.x, 0, currentInput.y).normalized;
        
        if(Vector3.Distance(mainCameraController.MidPoint, transform.position + inputDirection) > mainCameraController.WanderLimit){
            currentInput = Vector2.zero;  
        } 

        float targetSpeed = movementSpeed;

        if(currentInput == Vector2.zero) targetSpeed = 0f;

        float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z).magnitude;
        float speedOffset = 0.1f;

        if(currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset){
            currentSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * currentInput.magnitude, Time.deltaTime * accelerationSpeed);

            currentSpeed = Mathf.Round(currentSpeed * 1000f) / 1000f;
        }

        else{
            currentSpeed = targetSpeed;
        }
        var targetVector = Quaternion.Euler(0, mainCameraController.transform.eulerAngles.y, 0) * inputDirection;

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
