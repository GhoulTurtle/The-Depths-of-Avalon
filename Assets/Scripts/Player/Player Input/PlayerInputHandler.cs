using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour{
	private PlayerInputHandler inputActions;
	private PlayerMovement playerMovement;

	private void Awake(){
		TryGetComponent(out inputActions);
		TryGetComponent(out playerMovement);
	}

	public void OnMove(CallbackContext context){
		if(playerMovement == null) return;
		playerMovement.SetInputVector(context.ReadValue<Vector2>());
	}
}
