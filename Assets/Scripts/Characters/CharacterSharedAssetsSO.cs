using UnityEngine;

[CreateAssetMenu(menuName = "Character/Shared Asset", fileName = "CharacterSharedAsset")]
public class CharacterSharedAssetsSO : ScriptableObject{
    [Header("Status VFX")]
    public CharacterStatusVisualEffect BurnedStatusVFX;
    public CharacterStatusVisualEffect SlowedStatusVFX;
    public CharacterStatusVisualEffect KnockbackStatusVFX;
    public CharacterStatusVisualEffect StunnedStatusVFX;

    [Header("Status Audio")]
    public AudioEvent BurnedStatusAudioEvent;
    public AudioEvent SlowedStatusAudioEvent;
    public AudioEvent KnockbackStatusAudioEvent;
    public AudioEvent StunnedStatusAudioEvent;

    public void StatusInflicted(Status status, Character character){
        switch (status){
            case Status.Burned: 
                InstatiateVFX(BurnedStatusVFX, character);
                BurnedStatusAudioEvent.Play(character.CharacterAudioSource);
                break;
            case Status.Knockback: 
                InstatiateVFX(KnockbackStatusVFX, character);
                KnockbackStatusAudioEvent.Play(character.CharacterAudioSource);
                break;
            case Status.Slow: 
                InstatiateVFX(SlowedStatusVFX, character);
                SlowedStatusAudioEvent.Play(character.CharacterAudioSource);
                break;
            case Status.Stun: 
                InstatiateVFX(StunnedStatusVFX, character);
                StunnedStatusAudioEvent.Play(character.CharacterAudioSource);
                break;
        }
    }

    private void InstatiateVFX(CharacterStatusVisualEffect effect, Character character){
        if(effect != null){
            var visualEffect = Instantiate(effect, character.transform);
            visualEffect.SetupVisualEffect(character);
        }
    }
}   
