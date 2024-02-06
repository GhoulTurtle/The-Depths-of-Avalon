using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Fire Damage", menuName = "Damage Type/Fire Damage")]
public class FireDamageSO : DamageTypeSO
{
    public int TicksPerSecond;
    public int EffectDuration;
    public bool isInFlames;
    // DoT effect
    // Deals the damageAmount per tick

    public override void DealDamage(HealthSystem healthSystem, float damageAmount, StatusEffect statusEffect, Transform damageSource) {
        Debug.Log("Starting DealDamage");
        isInFlames = !isInFlames;

        if(isInFlames) {
            _ = DamageOverTime(healthSystem, damageAmount, damageSource);
        }
    }

    private async Task DamageOverTime(HealthSystem healthSystem, float damageAmount, Transform damageSource) {
        float timeBetweenTicks = 1f / TicksPerSecond;

        while (isInFlames) {
            // Apply damage each tick
            healthSystem.TakeDamage(this, damageAmount, damageSource);
            Debug.Log($"Damage tick");

            // Delay for the specified time between ticks
            await Task.Delay((int)(timeBetweenTicks * 1000));
        }
        if(!isInFlames) {
            // Continue ticks until the effect duration is complete
            for (int i = 0; i < TicksPerSecond * EffectDuration; i++) {
                // Apply damage each tick
                // Note: You may want to add a check here if isInFlames becomes true again during the continuation.
                healthSystem.TakeDamage(this, damageAmount, damageSource);
                Debug.Log($"Damage tick {i + 1}");

                // Delay for the specified time between ticks
                await Task.Delay((int)(timeBetweenTicks * 1000));
            }

            Debug.Log("Damage over time stopped");
        }
    }

}
