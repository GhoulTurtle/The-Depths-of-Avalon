using System;
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
		public CharacterStatsSO CharacterStats;
		public SetupCharacterEventArgs(CharacterStatsSO _characterStats){
			CharacterStats = _characterStats;
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

		OnSetupCharacter?.Invoke(this, new SetupCharacterEventArgs(characterSO.CharacterStats));
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
