//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

[CreateAssetMenu(menuName = "Character/Audio/Basic", fileName = "NewBasicCharacterAudioSO")]
public class CharacterAudioSO : ScriptableObject{
	public AudioEvent WalkingAudioEvent;
	public AudioEvent DamagedAudioEvent;
	public AudioEvent HealingAudioEvent;

	public void CharacterDamagedAudio(Character character){
		if(DamagedAudioEvent != null){
			DamagedAudioEvent.Play(character.CharacterAudioSource);
		}
	}

	public void CharacterHealedAudio(Character character){
		if(HealingAudioEvent != null){
			HealingAudioEvent.Play(character.CharacterAudioSource);
		}
	}
}
