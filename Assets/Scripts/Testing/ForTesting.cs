//Last Editor: Caleb Hussleman
//Last Edited: Feb 14

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTesting : MonoBehaviour {

    public HealthSystem healthSystem;
    public DamageTypeSO damageTypeSO;

    private void Awake() {

    }
    private void OnEnable() {
        
    }

    private void OnDisable() {

    }

    private void Start() {

    }

    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.K)) {
            healthSystem.TakeDamage(damageTypeSO, 20, this.transform);
        }
    }
}
