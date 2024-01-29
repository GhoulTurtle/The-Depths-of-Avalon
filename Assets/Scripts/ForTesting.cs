using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForTesting : MonoBehaviour {

    public HealthSystem healthSystem;

    private void Awake() {

    }
    private void OnEnable() {
        
    }

    private void OnDisable() {

    }

    private void Start() {

    }

    
    private void Update() {
        if(Input.GetKeyDown(KeyCode.J)) {
            healthSystem.TakeDamage(10);
        }
    }
}
