using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stun Damage", menuName = "Damage Type/Stun Damage")]
public class StunDamageSO : DamageTypeSO
{
    public override void DealDamage(HealthSystem healthSystem, float damageAmount) {
        // if(TryGetGetComponent<???>(out ??? nameHere)) {
        //     //Call the StunFunction from the component
        // }

        healthSystem.TakeDamage(damageAmount);
    }
}
