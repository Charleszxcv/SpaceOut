using UnityEngine;

public class UltiSkillDamager : MonoBehaviour {
    [System.Serializable]
    public class DamagerAmount {
        public int enemyDamagerAmount;
        public int bossDamagerAmount;
    }

    public DamagerAmount m_DamagerAmount;
    public float nextHitDuration;
    public bool isEnemySecondKilled = true;

    private float nextHitMoment = 0;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            if (collision.GetComponent<Enemy>() != null) {
                if (isEnemySecondKilled)
                    collision.GetComponent<Enemy>().DamageToDie();
                else {
                    collision.GetComponent<Enemy>().GetDamage(m_DamagerAmount.enemyDamagerAmount);
                }
            }
        } else if (collision.tag == "Boss") {
            if (collision.GetComponent<BossHealth>() != null) {
                if (Time.time > nextHitMoment) {
                    collision.GetComponent<BossHealth>().TakeDamage(m_DamagerAmount.bossDamagerAmount);
                    Debug.Log("ultiSkill hit boss");
                    nextHitMoment = Time.time + nextHitDuration;
                }
            }
        }
    }
}
