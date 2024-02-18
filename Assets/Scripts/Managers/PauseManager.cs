using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    [SerializeField] private bool isGamePaused;
    public GameObject pauseScreen;
    public GameObject gameplayUI;

    private void Start() {
        PlayerManager.Instance.OnCorrectPlayerCount += ActivatePause;
        gameplayUI.SetActive(true);
        pauseScreen.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1.0f;
    }

    private void ActivatePause(object sender, EventArgs e) {
        foreach(PlayerOptions playerOptions in FindObjectsOfType<PlayerOptions>()) {
            playerOptions.OnOptionsPressed += TogglePauseMenu;
        }
    }

    public void TogglePauseMenu(object sender , EventArgs e) {
        isGamePaused = !isGamePaused;
        pauseScreen.SetActive(isGamePaused);
        gameplayUI.SetActive(!isGamePaused);
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

}
