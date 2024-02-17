//Last Editor: Caleb Richardson
//Last Edited: Feb 15

using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Projectile/Basic Projectile Attack", fileName = "NewProjectileAbilitySO")]
public class ProjectileAbilitySO : AbilitySO{
    [Header("Projectile Abilites")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float projectileSpawnOffset;

    private float projectileSize = 0.25f;

    public override void CancelAbility(Caster caster){

    }

    public override void CastAbility(Caster caster){
        caster.DebugAbility(GizmosColor, GizmosShape, caster.transform.position + caster.transform.forward, Vector3.zero, projectileSize);
        Projectile projectile = Instantiate(projectilePrefab, caster.transform.position + caster.transform.forward * projectileSpawnOffset, Quaternion.identity);
        var randomDamage = Random.Range(AbilityDamageAmount.minValue, AbilityDamageAmount.maxValue);
        projectile.SetupProjectile(randomDamage, AbilityDamageType, AbilityStatusEffect, caster.transform.forward);
    }
}
