//Last Editor: Caleb Hussleman
//Last Edited: Feb 14

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathEventTest : MonoBehaviour {
    private PlayerRespawner playerRespawner => GetComponent<PlayerRespawner>();

    public GameObject GameOverUI;

    private void Start() {
        playerRespawner.OnGameOver += GameOver;
    }

    private void OnDestroy() {
        playerRespawner.OnGameOver -= GameOver;
    }

    private void GameOver(object sender, EventArgs e) {
        GameOverUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void RestartButton() {
        SceneManager.LoadScene(0);
    }
}
