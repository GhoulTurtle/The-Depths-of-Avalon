using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {
    [SerializeField] private HealthSystem healthSystem => GetComponent<HealthSystem>();
    [SerializeField] private float respawnDelay;
    public Transform respawnPoint;

    private void OnEnable() {
        healthSystem.OnDie += StartRespawn;
    }

    private void OnDisable() {
        healthSystem.OnDie -= StartRespawn;
    }

    private void StartRespawn(object sender, EventArgs e) {
        Debug.Log("Player died, starting respawn");
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn(){
        yield return new WaitForSeconds(respawnDelay);
        Debug.Log("Player Respawned");
        this.gameObject.transform.position = respawnPoint.position;
    }        
}
