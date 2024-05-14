using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Timer : MonoBehaviour {
    [System.Serializable]
    public class PassLevelEvent : UnityEvent { }

    [System.Serializable]
    public class TriggerMoments {
        public int sceneIndex;
        public int triggerMoment;
        public float enterNextLevelDelay;
        public PassLevelEvent passLevelEvent;
    }

    public TriggerMoments trigger;
    public Text m_DisplayText;

    private int elapsedTime = 0;

    private void Start() {
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer() {
        while (true) {
            if (SceneManager.GetActiveScene().buildIndex == trigger.sceneIndex) {
                if (elapsedTime >= trigger.triggerMoment) {
                    yield return new WaitForSeconds(trigger.enterNextLevelDelay);

                    trigger.passLevelEvent.Invoke();
                    break;
                }
            }

            elapsedTime += 1;
            yield return new WaitForSeconds(1f);
            m_DisplayText.text = elapsedTime.ToString();
        }
    }
}
