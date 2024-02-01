using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charge Damage", menuName = "Damage Type/Charge Damage")]
public class ChargeDamageSO : DamageTypeSO
{
    public override void DealDamage(HealthSystem healthSystem, float damageAmount) {
        healthSystem.TakeDamage(damageAmount);
    }
}
