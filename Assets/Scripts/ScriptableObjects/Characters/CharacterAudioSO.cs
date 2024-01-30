using UnityEngine;

[CreateAssetMenu(menuName = "Character/Audio/Basic", fileName = "NewBasicCharacterAudioSO")]
public class CharacterAudioSO : ScriptableObject{
	public AudioEvent WalkingAudioEvent;
	public AudioEvent GettingHurtAudioEvent;
	public AudioEvent HealingAudioEvent;
}
