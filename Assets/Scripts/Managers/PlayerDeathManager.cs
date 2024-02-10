using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeathManager : MonoBehaviour {

    private PlayerManager playerManager => GetComponent<PlayerManager>();
    public List<Player> playerList = new List<Player>();

    private void OnEnable() {
        playerManager.OnCorrectPlayerCount += CreatePlayerList;
    }
    
    private void OnDisable() {
        playerManager.OnCorrectPlayerCount -= CreatePlayerList;
    }

    private void CreatePlayerList(object sender, EventArgs e) {
        foreach(Player player in playerManager.CurrentPlayerList) {
            playerList.Add(player);
            HealthSystem healthSystem = player.playerCharacter.GetComponent<HealthSystem>();
            if(healthSystem != null) {
                Debug.Log(player.playerNum + " Has health system");
            }
        }
    }
}
