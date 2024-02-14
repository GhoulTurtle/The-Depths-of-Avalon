//Last Editor: Caleb Husselman
//Last Edited: Feb 14

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour {
    [SerializeField] private LayerMask playerLayer;
    public Transform RespawnPoint;

    public EventHandler<PlayerEnteredArgs> OnPlayerEntered;

    public class PlayerEnteredArgs : EventArgs {
        public Transform respawnTransform;

        public PlayerEnteredArgs(Transform _respawnTransform) {
            this.respawnTransform = _respawnTransform;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            Debug.Log("Player entered Zone");
            CheckpointManager.Instance.UpdateCurrentRespawnPoint(RespawnPoint);
        }
    }
}
