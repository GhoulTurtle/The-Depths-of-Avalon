//Last Editor: Caleb Husselman
//Last Edited: Feb 14

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    private HealthSystem healthSystem => GetComponent<HealthSystem>();
    private PlayerRespawner respawner => FindObjectOfType<PlayerRespawner>();
        
    private void Start() {
        healthSystem.OnDie += StartRespawn;
    }

    private void OnDestroy() {
        healthSystem.OnDie -= StartRespawn;
    }

    private void StartRespawn(object sender, System.EventArgs e) {
        Debug.Log("Player died, starting respawn");
        Player player = PlayerManager.Instance.CurrentPlayerList.Find(p => p.assignedPlayerInput.gameObject == gameObject);
        
        if(player != null) {
            respawner.StartRespawn(player);
        } else {
            Debug.LogWarning("Player Not Found In PlayerManager List");
        }
        
    }
}
