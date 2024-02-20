//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

[CreateAssetMenu(menuName = "Character/Shared Asset", fileName = "CharacterSharedAsset")]
public class CharacterSharedAssetsSO : ScriptableObject{
    [Header("Health VFX")]
    [Tooltip("The visual effect that will be used when the character is healed.")]
	public CharacterVisualEffect CharacterHealingEffect;
    [Tooltip("The visual effect that will be used when the character is damaged.")]
	public CharacterVisualEffect CharacterDamagedEffect;
    
    [Header("Status VFX")]
    public CharacterVisualEffect BurnedStatusVFX;
    public CharacterVisualEffect SlowedStatusVFX;
    public CharacterVisualEffect KnockbackStatusVFX;
    public CharacterVisualEffect StunnedStatusVFX;

    [Header("Health Audio")]
    public AudioEvent HealingAudioEvent;
    public AudioEvent DamagedAudioEvent;

    [Header("Status Audio")]
    public AudioEvent BurnedStatusAudioEvent;
    public AudioEvent SlowedStatusAudioEvent;
    public AudioEvent KnockbackStatusAudioEvent;
    public AudioEvent StunnedStatusAudioEvent;

    public void CharacterStatusInflicted(Status status, Character character){
        switch (status){
            case Status.Burned: 
                InstantiateVFX(BurnedStatusVFX, character);
                PlayAudioEvent(BurnedStatusAudioEvent, character);
                break;
            case Status.Knockback: 
                InstantiateVFX(KnockbackStatusVFX, character);
                PlayAudioEvent(KnockbackStatusAudioEvent, character);
                break;
            case Status.Slow: 
                InstantiateVFX(SlowedStatusVFX, character);
                PlayAudioEvent(SlowedStatusAudioEvent, character);
                break;
            case Status.Stun: 
                InstantiateVFX(StunnedStatusVFX, character);
                PlayAudioEvent(StunnedStatusAudioEvent, character);
                break;
        }
    }

    public void CharacterDamaged(Character character){
		InstantiateVFX(CharacterDamagedEffect, character);
        PlayAudioEvent(DamagedAudioEvent, character);
	}

	public void CharacterHealed(Character character){
        InstantiateVFX(CharacterHealingEffect, character);
        PlayAudioEvent(HealingAudioEvent, character);
	}

    private void InstantiateVFX(CharacterVisualEffect effect, Character character){
        if(effect != null){
            var visualEffect = Instantiate(effect, character.CharacterVisualParent);
            visualEffect.SetupVisualEffect(character);
        }
    }

    private void PlayAudioEvent(AudioEvent audioEvent, Character character){
        if(audioEvent != null){
            audioEvent.Play(character.CharacterAudioSource);
        }
    }
}   
