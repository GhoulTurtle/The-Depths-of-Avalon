using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlateReceiver : MonoBehaviour {
    public Animator anim;
    public PlateObject activatablePlate;
    [SerializeField] private string triggerName;

    private void OnEnable() {
        activatablePlate.OnActivate += OnPlateActivate;
        activatablePlate.OnDeactivate += OnPlateDeactivate;
    }

    private void OnDisable() {
        activatablePlate.OnActivate -= OnPlateActivate;
        activatablePlate.OnDeactivate -= OnPlateDeactivate;
    }

    private void OnPlateActivate(object sender, EventArgs e) {
        anim.SetTrigger(triggerName);
    }

    private void OnPlateDeactivate(object sender, EventArgs e) {
        Debug.Log("Door Closing");
        anim.SetTrigger(triggerName);
    }
}
