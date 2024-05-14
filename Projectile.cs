using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Tooltip("Damage which a projectile deals to another object. Integer")]
    public int damage;

    [Tooltip("Whether the projectile belongs to the ‘Enemy’ or to the ‘Player’")]
    public bool enemyBullet;

    [Tooltip("Whether the projectile is destroyed in the collision, or not")]
    public bool destroyedByCollision;

    public GameObject m_HitBossEffect;

    private void OnTriggerEnter2D(Collider2D collision) //when a projectile collides with another object
    {
        if (enemyBullet)
        {
            if (collision.tag == "Player") {
                if (collision.GetComponent<PlayerHealth>() != null) {
                    collision.GetComponent<PlayerHealth>().TakeDamage(damage);
                }

                if (destroyedByCollision)
                    Destruction();
            }
            else if (collision.tag == "Ultimate") {
                Destruction();
            }
            
        }

        else if (!enemyBullet)
        {
            if (collision.tag == "Enemy") {
                if (collision.GetComponent<Enemy>() != null) {
                    collision.GetComponent<Enemy>().GetDamage(damage);
                }
            }
            
            else if (collision.tag == "Boss") {
                if (collision.GetComponent<BossHealth>() != null) {
                    collision.GetComponent<BossHealth>().TakeDamage(damage);
                    PoolManager.GetInstance.ReuseObject(m_HitBossEffect, transform.position, Quaternion.identity);
                    Destruction();
                }
            }
        }
    }

    void Destruction() 
    {
        if (enemyBullet) {
            Destroy(gameObject);
        } else {
            if (GetComponent<PlayerProjectilePoolObject>() != null) {
                GetComponent<PlayerProjectilePoolObject>().Destroy();
            }
        }
    }
}