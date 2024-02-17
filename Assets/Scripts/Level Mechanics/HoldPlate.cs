//Last Editor: Caleb Richardson
//Last Edited: Feb 16
using System;
using UnityEngine;

public class HoldPlate : PlateObject {
    public override event EventHandler OnActivate;
    public override event EventHandler OnDeactivate;

    public override void Activate() {
        OnActivate?.Invoke(this, EventArgs.Empty);
    }

    public override void Deactivate() {
        OnDeactivate?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other) {
        if((activateLayer.value & 1 << other.gameObject.layer) != 0) {
            Activate();
        }
    }

    private void OnTriggerExit(Collider other) {
        if((activateLayer.value & 1 << other.gameObject.layer) != 0) {
            Deactivate();
        }
    }
}
