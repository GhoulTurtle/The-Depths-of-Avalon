//Last Editor: Caleb Husselman
//Last Edited: Feb 16

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityRecharge : MonoBehaviour
{
    private Slider abilityFill => GetComponentInChildren<Slider>();
    private Coroutine cooldownCoroutine;
    public AbilitySO Ability;

    public void UseAbility() {
        if (cooldownCoroutine != null) StopCoroutine(cooldownCoroutine);

        cooldownCoroutine = StartCoroutine(CooldownVisual());
    }

    private IEnumerator CooldownVisual() {
        float cooldownTime = Ability.Cooldown;
        float currentTime = 0f;

        abilityFill.value = 0;

        while (currentTime < cooldownTime) {
            currentTime += Time.deltaTime;
            abilityFill.value = currentTime / cooldownTime;
            yield return null;
        }

        abilityFill.value = 1;
    }
}

