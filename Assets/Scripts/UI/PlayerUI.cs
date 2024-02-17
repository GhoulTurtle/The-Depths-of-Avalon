//Last Editor: Caleb Richardson
//Last Edited: Feb 16

using System;
using UnityEngine;

public class PlayerUI : MonoBehaviour {
    [SerializeField] private bool isPlayerOne;

    public event EventHandler<SetupUIEventArgs> OnSetupUI;

    public class SetupUIEventArgs : EventArgs {
        public Player player;
        public SetupUIEventArgs(Player _player) {
            player = _player;
        }
    }

    private void Start() {
        PlayerManager.Instance.OnCorrectPlayerCount += StartUISetup;
    }

    private void OnDestroy() {
        PlayerManager.Instance.OnCorrectPlayerCount -= StartUISetup;
    }

    private void StartUISetup(object sender, EventArgs e) {
        var playerNumber = isPlayerOne ? 0 : 1;
        Player player = PlayerManager.Instance.CurrentPlayerList[playerNumber];
        OnSetupUI?.Invoke(this, new SetupUIEventArgs(player));
    }
}
