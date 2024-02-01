using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(HealthSystem))]
public abstract class DamagableObject : MonoBehaviour {
    private HealthSystem healthSystem => GetComponent<HealthSystem>();

    private void OnEnable() {
        healthSystem.OnDamaged += OnObjectDamaged;
        healthSystem.OnDie += OnObjectDestroyed;
    }

    private void OnDisable() {
        healthSystem.OnDamaged -= OnObjectDamaged;
        healthSystem.OnDie -= OnObjectDestroyed;
    }
    
    public abstract void OnObjectDamaged(object sender, HealthSystem.DamagedEventArgs e);

    public abstract void OnObjectDestroyed(object sender, EventArgs e);
}
