using UnityEngine;

[CreateAssetMenu(fileName = "Physical Damage", menuName = "Damage Type/Physical Damage")]
public class PhysicalDamageSO : DamageTypeSO
{
    // Instant Damage
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource) {
        healthSystem.TakeDamage(this, damageAmount, damageSource);
    }
}
