using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    public GameObject loadingPanel;
    public GameObject[] panelToBeDeactivated;
    public Slider loadingBar;
    public string loadedHintText = "Tap to continue...";

    public void ResetUI() {
        if (loadingPanel == null) {
            return;
        }

        loadingPanel.SetActive(false);
        loadingBar.transform.Find("LoadingProgText").GetComponent<Text>().text = 0 + "%";
        loadingBar.value = 0f;
    }

    private void Start() {
        ResetUI();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKeyDown(KeyCode.PageDown)) {
                LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
            } else if (Input.GetKeyDown(KeyCode.PageUp)) {
                LoadLevel(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }

    //  load the next level
    public void LoadNextLevelOnDelay(float delay) {
        Invoke("LoadNextLevel", delay);
    }

    public void Reload() {
        PanelSwitching();
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
    }
    
    //  load the first scene in build settings
    public void LoadLevelFromBeginning() {
        PanelSwitching();
        StartCoroutine(LoadLevelAsync(0));
    }

    //  load level based on para
    public void LoadLevel(int sceneIndex) {
        PanelSwitching();
        StartCoroutine(LoadLevelAsync(sceneIndex));
    }

    //  load the next level
    public void LoadNextLevel() {
        PanelSwitching();
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelAsync(int sceneIndex) {
        if (sceneIndex > SceneManager.sceneCountInBuildSettings - 1 || sceneIndex < 0) {
            sceneIndex = 0;
        }
        Debug.Log("LoadScene: " + sceneIndex);

        yield return new WaitForSeconds(0.2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); 
        operation.allowSceneActivation = false; 

        #region Solution 1

        #endregion

        #region Solution 2
        float displayProgress, destProgress;
        displayProgress = destProgress = 0;

        while (operation.progress < 0.9f) {
            destProgress = operation.progress * 100f;
            while (displayProgress < destProgress) {
                loadingBar.transform.Find("LoadingProgText").GetComponent<Text>().text = ++displayProgress + "%";

				
				loadingBar.value = displayProgress / 100f;
                
				yield return new WaitForEndOfFrame();
            }
        }

        destProgress = 100f;
        while (displayProgress < destProgress) {
            loadingBar.transform.Find("LoadingProgText").GetComponent<Text>().text = ++displayProgress + "%";

			loadingBar.value = displayProgress / 100f;

            yield return new WaitForEndOfFrame();
        }
        operation.allowSceneActivation = true;

        #endregion
    }

    private void PanelSwitching() {
        if (loadingPanel != null) {
            loadingPanel.SetActive(true);
        }

        for (int i = 0; i < panelToBeDeactivated.Length; i++) {
            panelToBeDeactivated[i].SetActive(false);
        }
    }
}
