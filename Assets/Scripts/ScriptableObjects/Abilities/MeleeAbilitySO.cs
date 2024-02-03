using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack/Default Melee Attack", fileName = "NewMeleeAbilitySO")]
public class MeleeAbilitySO : AbilitySO{
    public override void CancelAbility(Caster caster){

    }

    public override void CastAbility(Caster caster){
		Debug.Log(AbilityName);
    }
}
