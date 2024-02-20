//Last Editor: Caleb Richardson
//Last Edited: Feb 17

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
        Player player = PlayerManager.Instance.CurrentPlayerList.Find(p => p.assignedPlayerInput.gameObject == gameObject);
        
        if(player != null) {
            respawner.StartRespawn(player);
        } else {
            Debug.LogWarning("Player Not Found In PlayerManager List");
        }
        
    }
}
