using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class Player{
	public int playerNum;
	public PlayerInput assignedPlayerInput;
	public Character playerCharacter;
	
	public Player(int _playerNum, PlayerInput _assignedplayerInput){
		playerNum = _playerNum;
		assignedPlayerInput = _assignedplayerInput;
		
		assignedPlayerInput.TryGetComponent(out playerCharacter);
		if(playerCharacter == null){
			Debug.LogError("Player " + playerNum + " does not have a Character component attached to the game object.");
		}
	}

	public void UpdateCharacter(CharacterSO _characterSO) => playerCharacter.ChangeCharacter(_characterSO);
}
