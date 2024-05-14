using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelLoader))]
public class StartMenu : MonoBehaviour {

    public Button btn_Start;
    public Button btn_Exit;
    private LevelLoader m_LevelLoader;

    private void Awake() {
        m_LevelLoader = GetComponent<LevelLoader>();
    }

    private void OnEnable() {
        btn_Start.onClick.AddListener(OnStart);
        btn_Exit.onClick.AddListener(OnExit);
    }

    private void OnDisable() {
        btn_Start.onClick.RemoveListener(OnStart);
        btn_Exit.onClick.RemoveListener(OnExit);
    }

    private void OnStart() {
        m_LevelLoader.LoadNextLevel();
    }

    private void OnExit() {
        Application.Quit();
    }
}
