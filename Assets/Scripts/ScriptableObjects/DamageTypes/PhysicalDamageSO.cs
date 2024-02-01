using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Physical Damage", menuName = "Damage Type/Physical Damage")]
public class PhysicalDamageSO : DamageTypeSO
{
    // Instant Damage
    public override void DealDamage(HealthSystem healthSystem, float damageAmount) {
        healthSystem.TakeDamage(this, damageAmount);
    }
}
