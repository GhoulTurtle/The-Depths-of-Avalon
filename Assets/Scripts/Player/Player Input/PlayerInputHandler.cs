using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour{
	private PlayerInputHandler inputActions;
	private PlayerMovement playerMovement;
	private Caster playerCaster;
	private Character playerCharacter;

	private bool disableInput = false;

	private void Awake(){
		TryGetComponent(out inputActions);
		TryGetComponent(out playerMovement);
		TryGetComponent(out playerCaster);
		TryGetComponent(out playerCharacter);

		playerCharacter.OnStatusEffectApplied += (sender, e) => {Debug.Log("Stun Started"); if(e.abilityEffect.Status == Status.Stun) disableInput = true;};
		playerCharacter.OnStatusEffectFinished += (sender, e) => {Debug.Log("Stun Finished"); if(e.abilityEffect.Status == Status.Stun) disableInput = false;};
	}

	private void OnDestroy() {
		playerCharacter.OnStatusEffectApplied -= (sender, e) => {if(e.abilityEffect.Status == Status.Stun) disableInput = true;};
		playerCharacter.OnStatusEffectFinished -= (sender, e) => {if(e.abilityEffect.Status == Status.Stun) disableInput = false;};
	}

	public void OnMove(CallbackContext context){
		if(playerMovement == null || disableInput){
			playerMovement.SetInputVector(Vector2.zero);
			return;	
		} 
		playerMovement.SetInputVector(context.ReadValue<Vector2>());
	}

	public void OnAbilityOne(CallbackContext context){
		if(playerCaster == null || disableInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(0);
	}

	public void OnAbilityTwo(CallbackContext context){
		if(playerCaster == null || disableInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(1);
	}

	public void OnAbilityThree(CallbackContext context){
		if(playerCaster == null || disableInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(2);
	}
	
	public void OnAbilityFour(CallbackContext context){
		if(playerCaster == null || disableInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(3);
	}
}
