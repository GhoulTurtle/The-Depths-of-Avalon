using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelect : MonoBehaviour {
    public Button buttonPrefab;
    public Transform buttonParent;
    public float buttonSpacing = 10f;

    void Start() {
        float yOffset = 0f;

        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            // Spawn button for each scene
            Button newButton = Instantiate(buttonPrefab, buttonParent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
            
            RectTransform buttonRect = newButton.GetComponent<RectTransform>();
            buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, -yOffset);
            yOffset += buttonRect.sizeDelta.y + buttonSpacing;

            int index = i;
            newButton.onClick.AddListener(() => LoadScene(index));
        }
    }

    void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void BackButton() {
        this.gameObject.SetActive(false);
    }
}
