using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    [System.Serializable]
    public class SwitchPanelSetting {
        public GameObject[] m_ActivatedPanels;
        public GameObject[] m_DeactivatedPanels;
    }

    public SwitchPanelSetting GameOverPanelSetting;
    public SwitchPanelSetting GameWinPanelSetting; 
    public bool IsGameOver { get { return this.isGameOver; } }

    private LevelLoader m_LevelLoader;
    private bool isGameOver;
    private static GameController _instance;

    public static GameController GetInstance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameController>();
            }
            return _instance;
        }
    }

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
        }
        m_LevelLoader = GetComponent<LevelLoader>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Keypad0)) {
            OnGameOver();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            OnGameWin();
        }

        if (Input.GetKeyDown(KeyCode.Keypad2)) {
            OnLevelPassed();
        }

        if (!isGameOver) {
            return;
        }
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetKeyDown(KeyCode.Space)) {
            m_LevelLoader.Reload();
        }
#endif

#if UNITY_ANDROID
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            m_LevelLoader.Reload();
        }
#endif
    }

    //  游戏胜利
    public void OnGameWin() {
        if (isGameOver) {
            return;
        }
        Debug.Log("OnGameWin Invoked");
        PanelSwitching(GameWinPanelSetting);

        PlayerMoving.instance.DisableController();
        isGameOver = true;
    }

    //  游戏失败
    public void OnGameOver() {
        PanelSwitching(GameOverPanelSetting);
        
        PlayerMoving.instance.DisableController();
        isGameOver = true;
    }

    //  本关胜利
    public void OnLevelPassed() {
        StartCoroutine(PassLevelEffect());
    }

    IEnumerator PassLevelEffect() {
        PlayerMoving.instance.PlayerMoveToNextLevel();
        yield return new WaitForSeconds(2f);
        m_LevelLoader.LoadNextLevel();
    }

    //  重新开始
    public void OnRestart() {
        m_LevelLoader.LoadLevelFromBeginning();
    }

    private void PanelSwitching(SwitchPanelSetting switchPanelSetting) {
        for (int i = 0; i < switchPanelSetting.m_ActivatedPanels.Length; i++) {
            if (switchPanelSetting.m_DeactivatedPanels[i] != null) {
                switchPanelSetting.m_ActivatedPanels[i].SetActive(true);
            }
        }

        for (int i = 0; i < switchPanelSetting.m_DeactivatedPanels.Length; i++) {
            if (switchPanelSetting.m_DeactivatedPanels[i] != null) {
                switchPanelSetting.m_DeactivatedPanels[i].SetActive(false);
            }
        }
    }
}
