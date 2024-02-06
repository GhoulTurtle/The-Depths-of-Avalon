using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoPlate : PlateObject
{
    public override event EventHandler OnActivate;
    public override event EventHandler OnDeactivate;

    // List to keep track of players on the pressure plate
    private List<GameObject> playersOnPlate = new List<GameObject>();
    private bool isActivated = false;

    public override void Activate()
    {
        // Check if both players are on the pressure plate
        if (playersOnPlate.Count == 2)
        {
            // Trigger the activation event
            OnActivate?.Invoke(this, EventArgs.Empty);
            Debug.Log("Duo Plate Activated");
            isActivated = true;
        }
    }

    public override void Deactivate()
    {
        // Check if any player is on the pressure plate
        if (playersOnPlate.Count > 0)
        {
            // Trigger the deactivation event
            //OnDeactivate?.Invoke(this, EventArgs.Empty);
            Debug.Log("Duo Plate Deactivated");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isActivated) {
            return;
        }
        if((playerLayer.value & 1 << other.gameObject.layer) != 0) {
            // Add the player to the list
            playersOnPlate.Add(other.gameObject);
            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer.value & 1 << other.gameObject.layer) != 0)
        {
            // Remove the player from the list
            playersOnPlate.Remove(other.gameObject);
            Deactivate();
        }
    }
}
