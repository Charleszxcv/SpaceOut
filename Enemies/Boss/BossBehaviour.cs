using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class BossBehaviour : MonoBehaviour {
    public float moveToDuration = 3f;
    public Vector3 moveFrom;
    public Vector3 moveTo;

    private float m_TotalDistance;
    private float m_CurDistance;
    private float m_MoveToSpeed;
    private bool m_Invincible = false;
    private bool m_IsDefeated = false;

    private void Start() {
        transform.position = moveFrom;
        StartCoroutine(MoveTo(moveTo));
    }

    IEnumerator MoveTo(Vector3 moveToPos) {
        EnableInvicibility();

        m_TotalDistance = Vector2.Distance(transform.position, moveToPos);
        m_CurDistance = m_TotalDistance;
        m_MoveToSpeed = m_TotalDistance / moveToDuration;

        while (m_CurDistance > 0.01f) {
            transform.position = Vector2.MoveTowards(transform.position, moveToPos, m_MoveToSpeed * Time.deltaTime);
            m_CurDistance = Vector2.Distance(transform.position, moveToPos);

            yield return null;
        }

        DisableInvicibility();
        ArrivedTarPos();
    }

    private void ArrivedTarPos() {
        if (m_IsDefeated) {
            GameController.GetInstance.OnGameWin();
        }
    }

    public bool Invicible { get { return this.m_Invincible; } }

    public void EnableInvicibility() {
        m_Invincible = true;
    }

    public void DisableInvicibility() {
        m_Invincible = false;
    }

    public void MoveBackOnDie() {
        m_IsDefeated = true;
        StartCoroutine(MoveTo(moveFrom));
    }
}
