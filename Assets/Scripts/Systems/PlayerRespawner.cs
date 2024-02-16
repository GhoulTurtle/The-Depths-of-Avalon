//Last Editor: Caleb Hussleman
//Last Edited: Feb 14

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour {
    private CheckpointManager checkpointManager;
    public float RespawnDelay;

    public event EventHandler OnGameOver;
    public event EventHandler OnPlayerRespawned;

    public bool isOnePlayerDead;

    private void Start() {
        checkpointManager = CheckpointManager.Instance;
    }

    public void StartRespawn(Player player) {
        if(isOnePlayerDead) {
            Debug.Log("Game Over");
            player.assignedPlayerInput.gameObject.SetActive(false);
            OnGameOver?.Invoke(this, EventArgs.Empty);
        } else {
            isOnePlayerDead = true;
            StartCoroutine(Respawn(player, RespawnDelay));
        }
    }

    private IEnumerator Respawn(Player player, float respawnDelay) {
        player.assignedPlayerInput.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnDelay);

        Debug.Log("Player Respawned");
        OnPlayerRespawned?.Invoke(this, EventArgs.Empty);
        
        // Move the player to the respawn point
        player.assignedPlayerInput.transform.position = checkpointManager.CurrentRespawnPoint.position;

        
        player.assignedPlayerInput.gameObject.SetActive(true);
        player.assignedPlayerInput.GetComponent<HealthSystem>().Heal(1000);

        isOnePlayerDead = false;
    }
}
