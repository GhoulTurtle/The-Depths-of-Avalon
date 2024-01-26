using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerManager : MonoBehaviour{
	PlayerInputManager playerInputManager;

	private void Awake() {
		TryGetComponent(out playerInputManager);
	}

	
}
