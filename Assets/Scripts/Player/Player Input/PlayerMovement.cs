//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour{
    [Header("Speed Variables")]
    [Range(3, 20)]
    [SerializeField] private float movementSpeed = 5f;
    [Range(3, 30)]
    [SerializeField] private float rotationSpeed;

    [Header("Ground Check Variables")]
    [SerializeField] private float groundedOffset = -0.14f;
    [SerializeField] private float groundedRadius = 0.5f;
    [SerializeField] private LayerMask groundLayers;

    [Header("Gravity")]
    [SerializeField] private float gravity = -15f;
    
    private Vector2 currentInput;
    private CharacterController characterController;

    //Movement
    private Vector3 inputDirection;
    private Vector3 externalMovement;
    public float CurrentMovementSpeed => currentMovementSpeed;

    //Gravity & Mass
    private const float terminalVelocity = 53f;
    private float verticalVelocity;
    private float playerMass = 3f;
    private float knockbackEffectMinAmount = 0.5f;
    private float knockbackEnergyConsumptionAmount = 5f;
    private float defaultGravity;

    //Checks
    private bool grounded = false;

    //References
    private CameraController mainCameraController;
    private Character playerCharacter;

    //Movement speed
    private float currentMovementSpeed;
    
    private void Awake() {
        TryGetComponent(out characterController);
        TryGetComponent(out playerCharacter);
        playerCharacter.OnSetupCharacter += UpdateMovementVariables;
        playerCharacter.OnStatusEffectApplied += StatusEffectApplied;
        playerCharacter.OnStatusEffectFinished += StatusEffectFinished;

        defaultGravity = gravity;
    }

    private void OnDestroy() {
        playerCharacter.OnSetupCharacter -= UpdateMovementVariables;
        playerCharacter.OnStatusEffectApplied -= StatusEffectApplied;
        playerCharacter.OnStatusEffectFinished -= StatusEffectFinished;
    }

    private void Start() {
        Camera.main.TryGetComponent(out mainCameraController);
        
        currentMovementSpeed = movementSpeed;
    }

    private void Update() {
        GroundCheck();
        Gravity();
        Move();
        ExternalMovementEnergyCalc();
    }

    public void SetInputVector(Vector2 _currentInput) => currentInput = _currentInput;

    public void UpdatePosition(Vector3 pos){
        characterController.enabled = false;
        transform.position = pos;
        characterController.enabled = true;
    }

    public void SetMovementSpeed(float speed){
        currentMovementSpeed = speed;
    }

    public void ResetMovementSpeed(){
        currentMovementSpeed = movementSpeed;
    }

    public void AddExternalForce(Vector3 forceDirection, float forceStrength){
        externalMovement += forceDirection.normalized * (forceStrength / playerMass);
    }

    public void SetPlayerGravity(float amount){
        gravity = amount;
    }

    public void ResetPlayerGravity(){
        gravity = defaultGravity;
    }
    
    private void StatusEffectApplied(object sender, Character.StatusEffectAppliedEventArgs e){
        switch (e.abilityEffect.Status){
            case Status.Knockback:
                var direction = (e.damageSource.position + transform.position).normalized;
                direction.y = 0;
                AddExternalForce(direction, e.abilityEffect.statusStrength);
                break;
            case Status.Slow:
                float reducedMovementSpeed = movementSpeed * (1f - (e.abilityEffect.statusStrength * 0.01f));
                currentMovementSpeed = reducedMovementSpeed;
                break;
        }
    }

    private void StatusEffectFinished(object sender, Character.StatusEffectAppliedEventArgs e){
        if(e.abilityEffect.Status != Status.Slow) return;
        currentMovementSpeed = movementSpeed;
    }

    private void UpdateMovementVariables(object sender, Character.SetupCharacterEventArgs e){
        movementSpeed = e.CharacterStats.movementSpeed;
        playerMass = e.CharacterStats.characterMass;
        rotationSpeed = e.CharacterStats.rotationSpeed;
    }

    private void Gravity(){
        if(grounded){
            verticalVelocity = 0f;
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

        var targetVector = Quaternion.Euler(0, mainCameraController.transform.eulerAngles.y, 0) * inputDirection;

        Vector3 finalMove = targetVector + externalMovement;

        //Add vertical velocity to the calculation
        characterController.Move(finalMove * (currentMovementSpeed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
        UpdateRotation(targetVector);
    }

    private void UpdateRotation(Vector3 movementDirection){
        if(movementDirection.magnitude == 0) { return; }

        var rotation = Quaternion.LookRotation(movementDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
    }
    
    private void ExternalMovementEnergyCalc(){
        if (externalMovement == Vector3.zero) return;
        
        if (externalMovement.magnitude <= knockbackEffectMinAmount){
            externalMovement = Vector3.zero;
            return;
        }
        
        externalMovement = Vector3.Lerp(externalMovement, Vector3.zero, knockbackEnergyConsumptionAmount * Time.deltaTime);
    }
}