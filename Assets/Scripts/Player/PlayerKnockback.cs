//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

public class PlayerKnockback : MonoBehaviour{
	private float playerMass = 3f;

	private Vector3 impact = Vector3.zero;

	private CharacterController playerCharacterController;

	private void Awake() {
		TryGetComponent(out playerCharacterController);
	}

    public void AddKnockBackToPlayer(Vector3 direction, float knockBackStrength){
        direction.Normalize();
        if (direction.y < 0) direction.y = -direction.y; // reflect down force on the ground
        impact += direction.normalized * knockBackStrength / playerMass;
    }

	private void Update() {
		if(impact.magnitude > 0.2f){
			playerCharacterController.Move(impact * Time.deltaTime);
			// consumes the impact energy each cycle:
			impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
		} 
	}
}
