using UnityEngine;

[CreateAssetMenu(fileName = "Charge Damage", menuName = "Damage Type/Charge Damage")]
public class ChargeDamageSO : DamageTypeSO
{
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource) {
        healthSystem.TakeDamage(this, damageAmount, damageSource);
    }
}
