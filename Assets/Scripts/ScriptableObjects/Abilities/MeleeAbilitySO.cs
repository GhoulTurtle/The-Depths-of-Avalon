using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Attack/Default Melee Attack", fileName = "NewMeleeAbilitySO")]
public class MeleeAbilitySO : AbilitySO{
    [Header("Melee Ability Variables")]
    [Tooltip("How big is the range of the collider sphere to check for target?")]
    public float MeleeRangeRadius;
    [Tooltip("The offset of the sphere check from the caster. Bigger caster = Bigger Offset")]
    public float MeleeCheckOffset;
    [Tooltip("The maximum amount of targets the melee can deal damage to."), Range(1, 10)]
    public int MaxTargetsHit;

    private Collider[] targetColliders;
    
    public override void CancelAbility(Caster caster){

    }

    public override void CastAbility(Caster caster){
        //Set the maximum amount of targets the melee can hit
        targetColliders = new Collider[MaxTargetsHit];

        var meleePos = caster.transform.position + caster.transform.forward * MeleeCheckOffset;

        if(DrawAbilityGizmos) caster.DebugAbility(GizmosColor, GizmosShape, meleePos, Vector3.zero, MeleeRangeRadius);
		
        //Do a overlap sphere in front of the caster if results buffer returns 0 then return.
        if(Physics.OverlapSphereNonAlloc(meleePos, MeleeRangeRadius, targetColliders, caster.TargetLayerMask) == 0) return;
        
        //Go through each target and attempt to deal damage
        foreach (Collider target in targetColliders){
            if(target == null) continue;
            if(!target.TryGetComponent(out HealthSystem healthSystem)) continue;
            
            var randomDamage = Random.Range(AbilityDamageAmount.minValue, AbilityDamageAmount.maxValue);

            if(AbilityDamageType != null){
                AbilityDamageType.DealDamage(healthSystem, randomDamage, AbilityStatusEffect);
            }
            else{
                healthSystem.TakeDamage(AbilityDamageType, randomDamage);
            }
        }
    }
}