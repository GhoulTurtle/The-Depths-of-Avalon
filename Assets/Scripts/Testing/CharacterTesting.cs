//Last Editor: Caleb Richardson
//Last Edited: Feb 14

using UnityEngine;

public class CharacterTesting : MonoBehaviour{
	[SerializeField] private CharacterSO arthurCharacterSO;
	[SerializeField] private CharacterSO merlinCharacterSO;	

	private int selectedPlayer = 0;

	private void Update() {
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			selectedPlayer = 0;
			Debug.Log("Selected Player: 0");
		}
		
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			selectedPlayer = 1;
			Debug.Log("Selected Player: 1");
		}

		if(Input.GetKeyDown(KeyCode.Q)){
			//Update the selected player to be arthur
			PlayerManager.Instance.CurrentPlayerList[selectedPlayer].UpdateCharacter(arthurCharacterSO);
			Debug.Log(selectedPlayer + " is now arthur!");
		}

		if(Input.GetKeyDown(KeyCode.E)){
			//Update the selected player to be merlin
			PlayerManager.Instance.CurrentPlayerList[selectedPlayer].UpdateCharacter(merlinCharacterSO);
			Debug.Log(selectedPlayer + " is now merlin!");
		}
	}

}
