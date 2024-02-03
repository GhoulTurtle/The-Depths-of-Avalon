using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack/Charge Attack", fileName = "NewChargeAbilitySO")]
public class ChargeAbilitySO : AbilitySO
{
    public override void CancelAbility(Caster caster)
    {

    }

    public override void CastAbility(Caster caster)
    {
        Debug.Log(AbilityName);
    }
}
