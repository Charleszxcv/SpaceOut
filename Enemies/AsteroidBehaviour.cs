using System.Collections;
using UnityEngine;
using System;

public class AsteroidBehaviour : MonoBehaviour {

    #region Modifiable Public Variables
    public static event Action AsteroidReuse;

	public float tumble;

    public float fallingSpeed;
    
    public GameObject explosion;
    public GameObject playerExplosion;
    #endregion

    #region Private

    private AsteroidPoolObject m_AsteroidPoolObject;

    //  static instance
    private static AsteroidBehaviour _instance;
    #endregion

    //  static instacne getter
    public static AsteroidBehaviour GetInstace {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AsteroidBehaviour>();
            }
            return _instance;
        }
    }

    #region Unity life cycle
    private void Awake() {
        m_AsteroidPoolObject = GetComponent<AsteroidPoolObject>();
    }

    private void OnEnable() {
        RandomRotate(); 
        FallingDown();  
    }
    #endregion

    #region Contact Destroy

    public void GetDamageFromPlayerDamager() {
        if (explosion != null) {
            Instantiate(explosion, transform.position, transform.rotation);
        }
        DestructableHitting();
    }

    public void GetContactHitWithPlayer() {
        if (playerExplosion != null) {
            Instantiate(playerExplosion, transform.position, transform.rotation);
        }
        FindObjectOfType<PlayerHealth>().TakeDamage(1);
        DestructableHitting();
    }

    public void GetDestroyOnExitBoundary() {
        DestructableHitting();
    }

    private void DestructableHitting() {
        m_AsteroidPoolObject.Destroy(); 
        CallOnDestroy();  
    }
    
    private void CallOnDestroy() {
        if (AsteroidReuse != null) {
            AsteroidReuse();
        }
    }
    #endregion

    #region Movement
    private void RandomRotate() {
        GetComponent<Rigidbody>().angularVelocity = UnityEngine.Random.insideUnitSphere * tumble;
    }

    private void FallingDown() {
        GetComponent<Rigidbody>().linearVelocity = transform.up * fallingSpeed;
    }
    #endregion
}
