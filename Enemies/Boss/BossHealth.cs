using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {
    public float m_TotalHealth;
    public float CurrentHealth { get { return this.m_CurHealth; } }

    private BossBehaviour m_BossBehaviour;
    private Transform m_HealthBar;
    private SpriteRenderer m_HealthBarSpriteRenderer;
    private Vector3 m_InitBarScale;
    private float m_CurHealth;

    private void Awake() {
        m_BossBehaviour = GetComponent<BossBehaviour>();
        m_HealthBar = transform.Find("HealthBar");
        m_HealthBarSpriteRenderer = m_HealthBar.GetComponent<SpriteRenderer>();
    }

    private void Start() {
        m_CurHealth = m_TotalHealth;
        m_InitBarScale = m_HealthBar.localScale;
    }

    private void Update() {
        if (m_CurHealth <= 0) {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl)) {
            if (Input.GetKeyDown(KeyCode.K)) {
                Die();
            }
        }
    }

    public void TakeDamage(int damageAmount) {
        if (m_BossBehaviour != null && m_BossBehaviour.Invicible) {
            return;
        }

        if (m_CurHealth - damageAmount <= 0) {
            Die();
            return;
        }
        
        m_CurHealth -= damageAmount;
        UpdateHealthBar();
    }

    public void Die() {
        m_CurHealth = 0;
        m_BossBehaviour.MoveBackOnDie();
    }

    private void UpdateHealthBar() {
        m_HealthBarSpriteRenderer.material.color = Color.Lerp(Color.green, Color.red, 1 - m_CurHealth / m_TotalHealth);
        m_HealthBar.localScale = new Vector2(m_InitBarScale.x * (m_CurHealth / m_TotalHealth), m_InitBarScale.y);
    }
}
