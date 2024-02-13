using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InLevelCheckpointManager : MonoBehaviour {
    public static InLevelCheckpointManager Instance {get; private set;}
    public List<InLevelCheckpoint> checkpoints = new List<InLevelCheckpoint>();

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
