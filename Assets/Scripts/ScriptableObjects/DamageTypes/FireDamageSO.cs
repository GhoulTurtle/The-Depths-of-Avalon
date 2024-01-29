using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamageSO : DamageTypeSO
{
    // DoT effect
    public override void DealDamage(HealthSystem healthSystem, float damageAmount)
    {
        healthSystem.TakeDamage(damageAmount);
    }
}
