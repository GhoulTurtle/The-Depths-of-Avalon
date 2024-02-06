using System;
using UnityEngine;

public class PressPlate : PlateObject
{
    public override event EventHandler OnActivate;
    public override event EventHandler OnDeactivate;

    public override void Activate() {
        Debug.Log("Standing on Pressure Plate");
        OnActivate?.Invoke(this, EventArgs.Empty);
    }

    public override void Deactivate() {
        //No Deactivate for when its a Press Plate
    }

    private void OnTriggerEnter(Collider other) {
        if((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            Activate();
        }
    }
}
