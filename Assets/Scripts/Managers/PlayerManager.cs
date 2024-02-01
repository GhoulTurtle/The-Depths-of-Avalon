/// <summary>
/// A singleton that is responsible for handling our players joining/leaving and their respective characters.
/// Will need refactoring to support multiple scenes/levels so players don't have to rejoin every scene/level.
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour{
	[SerializeField] private CharacterSO arthurCharacterSO;
	[SerializeField] private CharacterSO merlinCharacterSO;

	public static PlayerManager Instance {get; private set;}

	public List<Player> CurrentPlayerList {get; private set;}

	public event EventHandler<PlayerEventArgs> OnPlayerJoined;
	public event EventHandler<PlayerEventArgs> OnPlayerLeave;
	public event EventHandler OnCorrectPlayerCount;

	private readonly float correctPlayerCount = 2;

	public class PlayerEventArgs : EventArgs{
		public Player player;
		public PlayerEventArgs(Player _player){
			player = _player;
		}
	}

	private void Awake() {
		CurrentPlayerList = new List<Player>();

		if(Instance == null){
			Instance = this;
		}
		else{
			Destroy(gameObject);
		}
	}

	public void AddNewPlayer(PlayerInput playerInput){
		int incomingPlayerIndex = CurrentPlayerList.Count;
		
		Player incomingPlayer = new Player(incomingPlayerIndex, playerInput);
		CurrentPlayerList.Add(incomingPlayer);

		var playerCharacter = incomingPlayerIndex == 0 ? arthurCharacterSO : merlinCharacterSO;

		incomingPlayer.UpdateCharacter(playerCharacter);

		OnPlayerJoined?.Invoke(this, new PlayerEventArgs(incomingPlayer));
		
		if(CurrentPlayerList.Count == correctPlayerCount){
			OnCorrectPlayerCount?.Invoke(this, EventArgs.Empty);
		}
    }

    public void RemovePlayer(PlayerInput playerInput){
		Player leavingPlayer = CurrentPlayerList.FirstOrDefault(x => x.assignedPlayerInput == playerInput);
		CurrentPlayerList.Remove(leavingPlayer);

		OnPlayerLeave?.Invoke(this, new PlayerEventArgs(leavingPlayer));
	}	
}
