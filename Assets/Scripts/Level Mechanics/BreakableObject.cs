//Last Editor: Caleb Richardson
//Last Edited: Feb 15
using System;

public class BreakableObject : DamagableObject{
    public override void OnObjectDamaged(object sender, HealthSystem.DamagedEventArgs e) {
    }

    public override void OnObjectDestroyed(object sender, EventArgs e) {
        Destroy(gameObject);
    }
}
