using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	private Dictionary<StatusEffect, IEnumerator> characterStatusDictionary;
	public GameObject characterVisuals;

	public event EventHandler<SetupCharacterEventArgs> OnSetupCharacter;
	public event EventHandler<StatusEffectAppliedEventArgs> OnStatusEffectApplied;
	public event EventHandler<StatusEffectAppliedEventArgs> OnStatusEffectFinished;

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
		public Transform damageSource;
		public StatusEffectAppliedEventArgs(StatusEffect _abilityEffect, Transform _damageSource = null){
			abilityEffect = _abilityEffect;
			damageSource = _damageSource;
		}
	}

	private HealthSystem characterHealthSystem;

	private void Awake() {
		characterStatusDictionary = new Dictionary<StatusEffect, IEnumerator>();
	}

	private void Start() {
		if(characterSO != null){
			SetupCharacter();
		}

		TryGetComponent(out characterHealthSystem);
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

	public void ApplyStatusEffectToCharacter(StatusEffect statusEffect, Transform damageSource){
		//Return if the character is dead
		if(!characterHealthSystem.IsAlive) return;

		//Check if we already have that effect
		if(characterStatusDictionary.FirstOrDefault(_statusEffect => _statusEffect.Key.Status == statusEffect.Status).Key != null){
			//Reset/Stack
			return;
		}

		//Make a new instance of a StatusEffect to be able to run a coroutine
		var statusEffectInstance = new StatusEffect(statusEffect.statusDuration, statusEffect.statusStrength, statusEffect.Status);
		var statusEffectCoroutine = statusEffectInstance.StatusEffectCoroutine(this);

		Debug.Log(statusEffect.Status + " has started!");

		characterStatusDictionary.Add(statusEffectInstance, statusEffectCoroutine);
		OnStatusEffectApplied?.Invoke(this, new StatusEffectAppliedEventArgs(statusEffect, damageSource));
		StartCoroutine(statusEffectCoroutine);
	}

	public void FinishedEffect(StatusEffect statusEffect){
		Debug.Log(statusEffect.Status + " has finished!");
		
		characterStatusDictionary.Remove(statusEffect);
		OnStatusEffectFinished?.Invoke(this, new StatusEffectAppliedEventArgs(statusEffect));
	}

	private void CleanupCharacter(){
		//Used when changing characters
		if(characterVisuals != null){
			Destroy(characterVisuals);
		}

		characterStatusDictionary?.Clear();
	}

    private void UpdateCharacterVisuals(){
		if(characterSO.CharacterVisuals.CharacterModelPrefab == null) return;
		characterVisuals = Instantiate(characterSO.CharacterVisuals.CharacterModelPrefab, transform.position, transform.rotation, characterVisualParent);
    }
}