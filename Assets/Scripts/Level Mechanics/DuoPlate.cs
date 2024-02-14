using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoPlate : PlateObject
{
    public override event EventHandler OnActivate;
    public override event EventHandler OnDeactivate;
    public int activatedCount = 0;

    public override void Activate() {
        Debug.Log("Plate Activating");
        activatedCount ++;
        if(activatedCount == 2) {
            OnActivate?.Invoke(this, EventArgs.Empty);
        }
    }

    public override void Deactivate() {
        activatedCount --;
        if(activatedCount <= 0) {
            activatedCount = 0;
        }
    }
}
