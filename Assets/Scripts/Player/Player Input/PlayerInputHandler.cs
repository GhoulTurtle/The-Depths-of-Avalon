//Last Editor: Caleb Richardson
//Last Edited: Feb 17

using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour{
	private PlayerInputHandler inputActions;
	private PlayerMovement playerMovement;
	private Caster playerCaster;
	private Character playerCharacter;
	private HealthSystem playerHeathSystem;
	private PlayerOptions playerOptions;

	//Switch to enum if needing more disableInputs
	private bool disableInput = false;
	private bool disableAbilityInput = false;

	private void Awake(){
		TryGetComponent(out inputActions);
		TryGetComponent(out playerMovement);
		TryGetComponent(out playerCaster);
		TryGetComponent(out playerCharacter);
		TryGetComponent(out playerHeathSystem);
		TryGetComponent(out playerOptions);

		playerHeathSystem.OnDie += (sender, e) => {disableInput = true;};
		playerHeathSystem.OnRespawn += (sender, e) => {disableInput = false;};
		playerCharacter.OnStatusEffectApplied += (sender, e) => {if(e.abilityEffect.Status == Status.Stun) disableInput = true;};
		playerCharacter.OnStatusEffectFinished += (sender, e) => {if(e.abilityEffect.Status == Status.Stun) disableInput = false;};
	}

	private void OnDestroy() {
		playerCharacter.OnStatusEffectApplied -= (sender, e) => {if(e.abilityEffect.Status == Status.Stun) disableInput = true;};
		playerCharacter.OnStatusEffectFinished -= (sender, e) => {if(e.abilityEffect.Status == Status.Stun) disableInput = false;};
		playerHeathSystem.OnDie -= (sender, e) => {disableInput = true;};
		playerHeathSystem.OnRespawn -= (sender, e) => {disableInput = false;};
	}

	public void SetDisableAbilityInput(bool state){
		disableAbilityInput = state;
	}

	public void OnMove(CallbackContext context){
		if(playerMovement == null || disableInput){
			playerMovement.SetInputVector(Vector2.zero);
			return;	
		} 
		playerMovement.SetInputVector(context.ReadValue<Vector2>());
	}

	public void OnAbilityOne(CallbackContext context){
		if(playerCaster == null || disableInput || disableAbilityInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(0);
	}

	public void OnAbilityTwo(CallbackContext context){
		if(playerCaster == null || disableInput || disableAbilityInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(1);
	}

	public void OnAbilityThree(CallbackContext context){
		if(playerCaster == null || disableInput || disableAbilityInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(2);
	}
	
	public void OnAbilityFour(CallbackContext context){
		if(playerCaster == null || disableInput || disableAbilityInput) return;
		if(context.phase == InputActionPhase.Performed) playerCaster.UseCharacterAbility(3);
	}

	public void OnOptionsCalled(CallbackContext context) {
		if(playerOptions == null || disableInput) return;
		if(context.phase == InputActionPhase.Performed) playerOptions.GetPlayer(playerCharacter);
	}
}
