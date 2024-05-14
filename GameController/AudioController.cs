using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    private static AudioController _instance;
    private AudioSource m_Audio;
    private bool m_Cheat = false;
    
    void Awake() {
        if (_instance == null) {
            _instance = this;
            m_Audio = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void OnEnable() {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }
    
    void Start() {
        if (m_Audio != null) {
            m_Audio.Play();
        }
    }

    private void OnDisable() {
        SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
    }

    public void SetPlayerInvincible() {
        m_Cheat = true;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1) {
        if (!m_Cheat) {
            return;
        }

        if (FindObjectOfType<PlayerHealth>() != null) {
            FindObjectOfType<PlayerHealth>().EnableInvincibilityByCheating();
        }
    }

}
