using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRespawner : MonoBehaviour {
    private InLevelCheckpointManager checkpointManager;
    public float RespawnDelay;

    public EventHandler GameOverEvent;

    public bool isOnePlayerDead;

    private void Start() {
        checkpointManager = InLevelCheckpointManager.Instance;
    }

    public void StartRespawn(Player player) {
        if(isOnePlayerDead) {
            Debug.Log("Game Over");
            player.assignedPlayerInput.gameObject.SetActive(false);
            GameOverEvent?.Invoke(this, EventArgs.Empty);
        } else {
            isOnePlayerDead = true;
            StartCoroutine(Respawn(player, RespawnDelay));
        }
    }

    private IEnumerator Respawn(Player player, float respawnDelay) {
        player.assignedPlayerInput.gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnDelay);

        Debug.Log("Player Respawned");
        
        
        // Move the player to the respawn point
        player.assignedPlayerInput.transform.position = checkpointManager.CurrentRespawnPoint.position;

        
        player.assignedPlayerInput.GetComponent<HealthSystem>().Heal(1000);
        player.assignedPlayerInput.gameObject.SetActive(true);

        isOnePlayerDead = false;
    }
}
