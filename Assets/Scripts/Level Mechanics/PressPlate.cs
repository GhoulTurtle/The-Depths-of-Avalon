//Last Editor: Caleb Husselman
//Last Edited: Feb 14

using System;
using UnityEngine;

public class PressPlate : PlateObject
{
    public override event EventHandler OnActivate;
    public override event EventHandler OnDeactivate;

    private bool isActivated = false;

    public override void Activate() {
        Debug.Log("Standing on Pressure Plate");
        OnActivate?.Invoke(this, EventArgs.Empty);
        isActivated = true;
    }

    public override void Deactivate() {
        //No Deactivate for when its a Press Plate
    }

    private void OnTriggerEnter(Collider other) {
        if(isActivated) {
            return;
        }
        if((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            Activate();
        }
    }
}
