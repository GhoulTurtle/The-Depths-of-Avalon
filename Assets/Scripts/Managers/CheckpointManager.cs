//Last Editor: Caleb Richardson
//Last Edited: Feb 17

using UnityEngine;

public class CheckpointManager : MonoBehaviour {
    public static CheckpointManager Instance {get; private set;}

    public Transform CurrentRespawnPoint;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void UpdateCurrentRespawnPoint(Transform respawnTransform) {
        CurrentRespawnPoint = respawnTransform;
        Debug.Log("Current Respawn Point updated to " + respawnTransform.position);
    }
}
