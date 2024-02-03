using UnityEngine;

[CreateAssetMenu(fileName = "Ice Damage", menuName = "Damage Type/Ice Damage")]
public class IceDamageSO : DamageTypeSO
{
    // Instant Damage.
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect) {
        healthSystem.TakeDamage(this, damageAmount);
    }
}
