using System.Collections;
using UnityEngine;

public class AbilityCooldown{
	public bool OnCooldown = false;
	public WaitForSecondsRealtime AbilityCooldownTimer;

	public AbilityCooldown(float abilityCooldown){
		AbilityCooldownTimer = new WaitForSecondsRealtime(abilityCooldown);
	}

	public IEnumerator AbilityCooldownCoroutine(){
		OnCooldown = true;
		yield return AbilityCooldownTimer;
		OnCooldown = false;
	}
}
