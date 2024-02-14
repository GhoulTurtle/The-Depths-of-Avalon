using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {
    public LayerMask KillableLayer;

    private void OnTriggerEnter(Collider other) {
        if((KillableLayer.value & 1 << other.gameObject.layer) != 0) {
            other.TryGetComponent(out HealthSystem healthSystem);
            if(healthSystem != null) {
                healthSystem.TakeDamage(null, 1000, null);
            }
        }
    }
}
