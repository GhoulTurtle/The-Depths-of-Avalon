using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BurnableGate : DamagableObject
{
    public override void OnObjectDamaged(object sender, HealthSystem.DamagedEventArgs e) {
        Debug.Log("Burnable Gate got burned for " + e.damageAmount);
    }

    public override void OnObjectDestroyed(object sender, EventArgs e) {
        Debug.Log("Burnable Gate burned to death");
        Destroy(this.gameObject);
    }
}
