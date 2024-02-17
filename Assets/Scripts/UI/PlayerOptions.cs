using System;
using UnityEngine;

public class PlayerOptions : MonoBehaviour {
    
    public event EventHandler OnOptionsPressed;

    public void GetPlayer(Character character) {
        Debug.Log(character);
        TogglePauseMenu();
    }

    public void TogglePauseMenu() {
        OnOptionsPressed?.Invoke(this, EventArgs.Empty);
    }
}
