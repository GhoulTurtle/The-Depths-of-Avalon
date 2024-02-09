using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour {
    private PlayerManager playerManager => GetComponent<PlayerManager>();
    public List<Player> playerList = new List<Player>();
    public bool IsOnePlayerDead;
    [SerializeField] private float respawnTimer;

    private void OnEnable() {
        playerManager.OnCorrectPlayerCount += CreatePlayerList;
    }
    
    private void OnDisable() {
        playerManager.OnCorrectPlayerCount -= CreatePlayerList;
    }

    private void Update() {

    }

    private void CreatePlayerList(object sender, EventArgs e) {
        foreach(Player player in playerManager.CurrentPlayerList) {
            playerList.Add(player);
            HealthSystem healthSystem = player.playerCharacter.GetComponent<HealthSystem>();
            if(healthSystem != null) {
                Debug.Log(player + " Has health system");
                healthSystem.OnDie += PlayerDied;
            }
        }
    }

    private void PlayerDied(object sender, EventArgs e) {
        HealthSystem healthSystem = sender as HealthSystem;

        if(healthSystem != null) {
            if(IsOnePlayerDead) {
                Time.timeScale = 0f;
                Debug.Log("Game Over");
            } else {
                Debug.Log("Player died, starting respawn");
                IsOnePlayerDead = true;
                Transform playerDeathSpot = healthSystem.gameObject.transform;
                Debug.Log(playerDeathSpot.position);
                StartCoroutine(PlayerRespawn(healthSystem.gameObject, respawnTimer, playerDeathSpot));
            }
        }
    }

    private IEnumerator PlayerRespawn(GameObject playerObject, float respawnDelay, Transform respawnPoint) {
        yield return new WaitForSeconds(respawnDelay);

        HealthSystem healthSystem = playerObject.GetComponent<HealthSystem>();
        if(healthSystem != null) {
            playerObject.transform.position = respawnPoint.position;
            healthSystem.Heal(100);
            Debug.Log(respawnPoint.position);
        }

        IsOnePlayerDead = false;
    }
}
