//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/AOE/Caster AOE Attack", fileName = "NewCasterAOEAbilitySO")]
public class CasterAOEAbilitySO : AbilitySO
{
    public override void CancelAbility(Caster caster){

    }

    public override void CastAbility(Caster caster){
        Debug.Log(AbilityName);
    }

}
