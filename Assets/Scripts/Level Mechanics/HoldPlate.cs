using System;
using UnityEngine;

public class HoldPlate : PlateObject {
    public override event EventHandler OnActivate;
    public override event EventHandler OnDeactivate;

    // Add a counter to keep track of the number of players on the plate
    private int playerCount = 0;

    public override void Activate() {
        OnActivate?.Invoke(this, EventArgs.Empty);
    }

    public override void Deactivate() {
        OnDeactivate?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other) {
        if((activateLayer.value & 1 << other.gameObject.layer) != 0) {
            playerCount++;
            if (playerCount == 1) {
                Activate();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if((activateLayer.value & 1 << other.gameObject.layer) != 0) {
            playerCount--;
            if (playerCount == 0) {
                Deactivate();
            }
        }
    }
}
