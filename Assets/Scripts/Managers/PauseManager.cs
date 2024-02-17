using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    [SerializeField] private bool isGamePaused;
    public GameObject pauseScreen;

    private void Start() {
        PlayerManager.Instance.OnCorrectPlayerCount += ActivatePause;
        pauseScreen.SetActive(false);
        isGamePaused = false;
    }

    private void ActivatePause(object sender, EventArgs e) {
        foreach(PlayerOptions playerOptions in FindObjectsOfType<PlayerOptions>()) {
            playerOptions.OnOptionsPressed += TogglePauseMenu;
        }
    }

    public void TogglePauseMenu(object sender , EventArgs e) {
        isGamePaused = !isGamePaused;
        pauseScreen.SetActive(isGamePaused);
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

}
