using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour{
    public bool isPaused = false;
    public GameObject pauseMenu;
    
    private void Start(){
      Time.timeScale = 1;
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false; 
    }

    private void Update(){
        if (Input.GetKey(KeyCode.Escape)){
            if(!isPaused){
                GamePaused();
                isPaused = true;
            }
        }
    }

    private void GamePaused(){
      Time.timeScale = 0;
      pauseMenu.SetActive(true); 
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
    }
    
    public void GameUnPaused(){
      Time.timeScale = 1;
      pauseMenu.SetActive(false);
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
      isPaused = false;
    }

    public void MainMenu(){
      SceneManager.LoadScene("Main Menu");
    }
      
  public void Quit(){
    Application.Quit();
  }
}
