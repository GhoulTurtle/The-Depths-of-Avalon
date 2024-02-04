using UnityEngine;

[CreateAssetMenu(fileName = "Ice Damage", menuName = "Damage Type/Ice Damage")]
public class IceDamageSO : DamageTypeSO
{
    // Instant Damage.
    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect) {
        if(healthSystem.TryGetComponent(out Character character) && statusEffect != null){
            statusEffect.SetStatus(Status.Slow);
            character.ApplyStatusEffectToCharacter(statusEffect);
        }
        healthSystem.TakeDamage(this, damageAmount);
    }
}
