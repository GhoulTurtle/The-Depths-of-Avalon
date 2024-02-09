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

	public Character character;
	private List<AbilitySO> abilitySOs;
	private Dictionary<AbilitySO, AbilityCooldown> abilityCooldownDictionary;

	private bool isCasting = false;
	private GizmosCall currentGizmosCall;

	private void Awake() {
		abilityCooldownDictionary = new Dictionary<AbilitySO, AbilityCooldown>();
		abilitySOs = new List<AbilitySO>();
		TryGetComponent(out character);
		
		character.OnSetupCharacter += SetupCharacterAbilities;
	}

    private void OnDestroy() {
		character.OnSetupCharacter -= SetupCharacterAbilities;
		StopAllCoroutines();
	}

    public void UseCharacterAbility(int abilityIndex){
		if(abilitySOs == null || abilitySOs.Count - 1 < abilityIndex) return;
		if(abilitySOs[abilityIndex] == null){
			Debug.LogError("<color=red>No valid ability found on character: " + character.gameObject.name + " that matches the index: " + abilityIndex + "</color>");
			return;
		}

		AbilityCooldown abilityCooldown = abilityCooldownDictionary[abilitySOs[abilityIndex]];

		if(abilityCooldown.OnCooldown || isCasting){
			if(abilitySOs[abilityIndex].IsCancelable){
				abilitySOs[abilityIndex].CancelAbility(this);
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

		AbilityCooldown abilityCooldown = abilityCooldownDictionary[ability];
		
		var abilityIndex = abilitySOs.IndexOf(ability);

		if(abilityCooldown.OnCooldown || isCasting){
			if(abilitySOs[abilityIndex].IsCancelable){
				abilitySOs[abilityIndex].CancelAbility(this);
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

		AbilityCooldown abilityCooldown = abilityCooldownDictionary[ability];

		var abilityIndex = abilitySOs.IndexOf(ability);

		abilitySOs[abilityIndex].CancelAbility(this);

		OnAbilityCanceled?.Invoke(this, new AbilityEventArgs(abilityCooldown));
	}

	public void DebugAbility(Color gizmosColor, GizmosShape shape, Vector3 origin, Vector3 destination = new Vector3(), float radius = 0){
		currentGizmosCall = new GizmosCall(gizmosColor, shape, origin, destination, radius);
    }

    private void SetupCharacterAbilities(object sender, Character.SetupCharacterEventArgs e){
		abilitySOs.Clear();
		abilityCooldownDictionary.Clear();

		abilitySOs = e.CharacterAbilities;

		foreach (AbilitySO ability in e.CharacterAbilities){
			abilityCooldownDictionary.Add(ability, new AbilityCooldown(ability.Cooldown));
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

	private void OnDrawGizmos() {
		if(currentGizmosCall == null) return;
		Gizmos.color = currentGizmosCall.Color;
        switch (currentGizmosCall.Shape){
            case GizmosShape.Ray: Gizmos.DrawRay(currentGizmosCall.Origin, currentGizmosCall.Destination);
                break;
            case GizmosShape.Sphere: Gizmos.DrawWireSphere(currentGizmosCall.Origin, currentGizmosCall.Radius);
                break;
            case GizmosShape.Box: Gizmos.DrawWireCube(currentGizmosCall.Origin, currentGizmosCall.Destination);
                break;
        }
	}
}