using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for triggering our abilities and keeping track of the abilites cooldown
/// </summary>

public class Caster : MonoBehaviour{
	public LayerMask CasterLayerMask => casterLayerMask;
	public LayerMask TargetLayerMask => targetLayerMask;

	public event EventHandler<AbilityEventArgs> OnAbilityCasted;
	public event EventHandler<AbilityEventArgs> OnAbilityFired;
	public event EventHandler<AbilityEventArgs> OnAbilityFailed;
	public event EventHandler<AbilityEventArgs> OnAbilityCanceled;
	
	public class AbilityEventArgs : EventArgs{
		public AbilityCooldown abilityCooldown;
		public AbilityEventArgs(AbilityCooldown _abilityCooldown){
			abilityCooldown = _abilityCooldown;
		}
	}

	[SerializeField] private LayerMask casterLayerMask;
	[SerializeField] private LayerMask targetLayerMask;

	private Character character;
	private List<AbilitySO> abilitySOs;
	private Dictionary<AbilitySO, AbilityCooldown> abilityCooldowns;

	private bool isCasting = false;

	private void Awake() {
		abilityCooldowns = new Dictionary<AbilitySO, AbilityCooldown>();
		abilitySOs = new List<AbilitySO>();
		TryGetComponent(out character);
		
		character.OnSetupCharacter += SetupCharacterAbilities;
	}

    private void OnDestroy() {
		character.OnSetupCharacter -= SetupCharacterAbilities;
	}

    public void UseCharacterAbility(int abilityIndex){
		if(abilitySOs[abilityIndex] == null){
			Debug.LogError("<color=red>No valid ability found on character: " + character.gameObject.name + " that matches the index: " + abilityIndex + "</color>");
			return;
		}

		AbilityCooldown abilityCooldown = abilityCooldowns[abilitySOs[abilityIndex]];

		if(abilityCooldown.OnCooldown || isCasting){
			if(abilitySOs[abilityIndex].IsCancelable){
				OnAbilityCanceled?.Invoke(this, new AbilityEventArgs(abilityCooldown));
				return;
			}
	
			OnAbilityFailed?.Invoke(this, new AbilityEventArgs(abilityCooldown));
			return;
		}

		OnAbilityCasted?.Invoke(this, new AbilityEventArgs(abilityCooldown));
		StartCoroutine(CastingCoroutine(abilitySOs[abilityIndex], abilityCooldown));
	}

	public void UseCharacterAbility(AbilitySO ability){
		if(!abilitySOs.Contains(ability)){
			Debug.LogError("<color=red>No valid ability found on character: " + character.gameObject.name + " that matches the ability: " + ability.AbilityName + "</color>");
			return;
		}

		AbilityCooldown abilityCooldown = abilityCooldowns[ability];
		
		var abilityIndex = abilitySOs.IndexOf(ability);

		if(abilityCooldown.OnCooldown || isCasting){
			if(abilitySOs[abilityIndex].IsCancelable){
				OnAbilityCanceled?.Invoke(this, new AbilityEventArgs(abilityCooldown));
				return;
			}
			Debug.Log(ability + " is on cooldown!");
			OnAbilityFailed?.Invoke(this, new AbilityEventArgs(abilityCooldown));
			return;
		}

		OnAbilityCasted?.Invoke(this, new AbilityEventArgs(abilityCooldown));
		StartCoroutine(CastingCoroutine(abilitySOs[abilityIndex], abilityCooldown));
	}

	public void CancelCharacterAbility(AbilitySO ability){
		if(!abilitySOs.Contains(ability)){
			Debug.LogError("<color=red>No valid ability found on character: " + character.gameObject.name + " that matches the ability: " + ability.AbilityName + "</color>");
			return;
		}

		AbilityCooldown abilityCooldown = abilityCooldowns[ability];

		var abilityIndex = abilitySOs.IndexOf(ability);

		abilitySOs[abilityIndex].CancelAbility(this);

		OnAbilityCanceled?.Invoke(this, new AbilityEventArgs(abilityCooldown));
	}
	 
	private void SetupCharacterAbilities(object sender, Character.SetupCharacterEventArgs e){
		abilitySOs.Clear();
		abilityCooldowns.Clear();

		abilitySOs = e.CharacterAbilities;

		foreach (AbilitySO ability in e.CharacterAbilities){
			abilityCooldowns.Add(ability, new AbilityCooldown(ability.Cooldown));
		}
    }

	private IEnumerator CastingCoroutine(AbilitySO ability, AbilityCooldown abilityCooldown){
		isCasting = true;
		yield return new WaitForSecondsRealtime(ability.CastTime);
		isCasting = false;

		var abilityIndex = abilitySOs.IndexOf(ability);

		abilitySOs[abilityIndex].CastAbility(this);

		StartCoroutine(abilityCooldown.AbilityCooldownCoroutine());
		OnAbilityFired?.Invoke(this, new AbilityEventArgs(abilityCooldown));
	}
}
