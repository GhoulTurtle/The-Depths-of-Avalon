using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour{
	private PlayerInputHandler inputActions;
	private PlayerMovement playerMovement;
	private Caster playerCaster;

	private void Awake(){
		TryGetComponent(out inputActions);
		TryGetComponent(out playerMovement);
		TryGetComponent(out playerCaster);
	}

	public void OnMove(CallbackContext context){
		if(playerMovement == null) return;
		playerMovement.SetInputVector(context.ReadValue<Vector2>());
	}

	public void OnAbilityOne(CallbackContext context){
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(0);
	}

	public void OnAbilityTwo(CallbackContext context){
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(1);
	}

	public void OnAbilityThree(CallbackContext context){
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(2);
	}
	
	public void OnAbilityFour(CallbackContext context){
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(3);
	}
}
