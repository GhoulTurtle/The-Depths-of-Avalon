//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

public class DeathZone : MonoBehaviour {
    public LayerMask KillableLayer;

    private void OnTriggerEnter(Collider other) {
        if((KillableLayer.value & 1 << other.gameObject.layer) != 0) {
            other.TryGetComponent(out HealthSystem healthSystem);
            if(healthSystem != null) {
                healthSystem.Die();
            }
        }
    }
}
