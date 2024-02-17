//Last Editor: Caleb Richardson
//Last Edited: Feb 14

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
	public CharacterSO characterSO;
	[SerializeField] private CharacterType characterType;

	[Header("Required References")]
	[SerializeField] private Transform characterVisualParent;
	[SerializeField] private AudioSource characterAudioSource;

	private Dictionary<StatusEffect, IEnumerator> characterStatusDictionary;
	
	public Transform CharacterVisualParent => characterVisualParent;
	public AudioSource CharacterAudioSource => characterAudioSource;
	public CharacterType CharacterType => characterType;
	public Animator CharacterAnimator;

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
	private GameObject characterVisuals;

	private void Awake() {
		characterStatusDictionary = new Dictionary<StatusEffect, IEnumerator>();
	}

	private void Start() {
		if(characterSO != null){
			SetupCharacter();
		}

		TryGetComponent(out characterHealthSystem);
		characterHealthSystem.OnDamaged += CharacterDamaged;
		characterHealthSystem.OnHealed += CharacterHealed;
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

	public void ApplyStatusEffectToCharacter(StatusEffect statusEffect, Transform statusSource){
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

		characterStatusDictionary.Add(statusEffectInstance, statusEffectCoroutine);

		if(characterSO.SharedAssetsSO != null){
			characterSO.SharedAssetsSO.CharacterStatusInflicted(statusEffectInstance.Status, this);
		}

		OnStatusEffectApplied?.Invoke(this, new StatusEffectAppliedEventArgs(statusEffect, statusSource));
		StartCoroutine(statusEffectCoroutine);
	}

	public void FinishedEffect(StatusEffect statusEffect){
		characterStatusDictionary.Remove(statusEffect);
		OnStatusEffectFinished?.Invoke(this, new StatusEffectAppliedEventArgs(statusEffect));
	}

	public void RemoveAllEffects(){
		foreach (var keyValuePair in characterStatusDictionary){
			StopCoroutine(keyValuePair.Value);
			keyValuePair.Key.StopStatusEffectCoroutine(this);
		}
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

    private void CharacterDamaged(object sender, HealthSystem.DamagedEventArgs e){
		if(characterSO.SharedAssetsSO != null){
			characterSO.SharedAssetsSO.CharacterDamaged(this);
		}
    }

	private void CharacterHealed(object sender, EventArgs e){
		if(characterSO.SharedAssetsSO != null){
			characterSO.SharedAssetsSO.CharacterHealed(this);
		}
	}
}