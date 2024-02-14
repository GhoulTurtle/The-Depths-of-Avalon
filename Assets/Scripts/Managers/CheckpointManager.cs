//Last Editor: Caleb Husselman
//Last Edited: Feb 14

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckpointManager : MonoBehaviour {
    public static CheckpointManager Instance {get; private set;}
    public List<Checkpoint> checkpoints = new List<Checkpoint>();

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
