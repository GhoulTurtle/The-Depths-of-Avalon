//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

[CreateAssetMenu(fileName = "Physical Damage", menuName = "Damage Type/Physical Damage")]
public class PhysicalDamageSO : DamageTypeSO{
    // Instant Damage
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource) {
        if(!healthSystem.IsAlive) return;
        healthSystem.TakeDamage(this, damageAmount, damageSource);
    }
}
