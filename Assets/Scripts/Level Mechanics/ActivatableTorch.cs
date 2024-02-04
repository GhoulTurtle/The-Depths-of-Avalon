using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableTorch : DamagableObject
{
    public override void OnObjectDamaged(object sender, HealthSystem.DamagedEventArgs e) {
        GameObject lightSource = this.transform.GetChild(0).gameObject;
        if(!lightSource.activeInHierarchy) {
            lightSource.SetActive(true);
            Debug.Log("Light Source Turning On");
        }
    }

    public override void OnObjectDestroyed(object sender, EventArgs e) {

    }
}
