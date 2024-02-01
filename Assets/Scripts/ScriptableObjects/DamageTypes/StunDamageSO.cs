using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun Damage", menuName = "Damage Type/Stun Damage")]
public class StunDamageSO : DamageTypeSO
{
    public override void DealDamage(HealthSystem healthSystem, float damageAmount) {
        // if(healthSystem.TryGetComponent(out Character character)){
        //     character.ApplyStatusEffectToCharacter(StatusEffect.Stun);
        // }

        healthSystem.TakeDamage(this, damageAmount);
    }
}
