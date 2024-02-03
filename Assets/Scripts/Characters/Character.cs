using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for setting up a character and updating the character at runtime.
/// </summary>

public class Character : MonoBehaviour{
	[Header("Character SO References")]
	[SerializeField] private CharacterSO characterSO;
	[SerializeField] private CharacterType characterType;

	[Header("Required References")]
	[SerializeField] private Transform characterVisualParent;
	[SerializeField] private AudioSource characterAudioSource;

	private GameObject characterVisuals;

	public event EventHandler<SetupCharacterEventArgs> OnSetupCharacter;
	public event EventHandler<StatusEffectAppliedEventArgs> OnStatusEffectApplied;

	public class SetupCharacterEventArgs : EventArgs{
		public List<AbilitySO> CharacterAbilities;
		public CharacterAudioSO CharacterAudio;
		public CharacterVisualsSO CharacterVisuals;
		public CharacterStatsSO CharacterStats;
		
		public SetupCharacterEventArgs(CharacterStatsSO _characterStats, CharacterVisualsSO _characterVisuals, CharacterAudioSO _characterAudio, List<AbilitySO> _characterAbilities){
			CharacterStats = _characterStats;
			CharacterVisuals = _characterVisuals;
			CharacterAudio = _characterAudio;
			CharacterAbilities = _characterAbilities;
		}
	}

	public class StatusEffectAppliedEventArgs : EventArgs{
		public StatusEffect abilityEffect;
		public StatusEffectAppliedEventArgs(StatusEffect _abilityEffect){
			abilityEffect = _abilityEffect;
		}
	}

	private void Start() {
		if(characterSO != null){
			SetupCharacter();
		}
	}

	public void ChangeCharacter(CharacterSO _characterSO){
		if(characterSO == _characterSO) return;

		characterSO = _characterSO;
		CleanupCharacter();
		SetupCharacter();
	}

	public void SetupCharacter(){
		//Setup Stats, Visuals, and Audio.
		UpdateCharacterVisuals();

		OnSetupCharacter?.Invoke(this, new SetupCharacterEventArgs(characterSO.CharacterStats, characterSO.CharacterVisuals, characterSO.CharacterAudio, characterSO.CharacterAbilities));
	}

	public void ApplyStatusEffectToCharacter(StatusEffect effect){
		OnStatusEffectApplied?.Invoke(this, new StatusEffectAppliedEventArgs(effect));
	}

	private void CleanupCharacter(){
		//Used when changing characters
		if(characterVisuals != null){
			Destroy(characterVisuals);
		}
	}

    private void UpdateCharacterVisuals(){
		if(characterSO.CharacterVisuals.CharacterModelPrefab == null) return;
		characterVisuals = Instantiate(characterSO.CharacterVisuals.CharacterModelPrefab, transform.position, transform.rotation, characterVisualParent);
    }
}