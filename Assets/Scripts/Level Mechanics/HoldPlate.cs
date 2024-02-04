using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldPlate : PlateObject {
    public override event EventHandler OnActivate;
    public override event EventHandler OnDeactivate;

    public override void Activate() {
        Debug.Log("Standing on Pressure Plate");
        OnActivate?.Invoke(this, EventArgs.Empty);
    }

    public override void Deactivate() {
        Debug.Log("Got off Pressure Plate");
        OnDeactivate?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other) {
        if((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            Activate();
        }
    }

    private void OnTriggerExit(Collider other) {
        if((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            Deactivate();
        }
    }
}
