using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PoolInfo {
    public GameObject prefab;
    public int poolSize;
}

public class PoolSpawner : MonoBehaviour {
    static PoolSpawner _instance;
    
    public PoolInfo m_PoolInfo;
    public bool spawnPoolOnStart = false;
    
    public static PoolSpawner GetInstance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<PoolSpawner>();

                if (_instance == null) {
                    GameObject go_PoolSpawner = new GameObject("PoolSpawner");
                    go_PoolSpawner.AddComponent<PoolSpawner>();
                    DontDestroyOnLoad(go_PoolSpawner);
                }
            }
            return _instance;
        }
    }

    protected virtual void Start() {
        DontDestroyOnLoad(gameObject);

        if (spawnPoolOnStart) {
            SpawnObjectPool();
        }
    }

    public void StartSpawning() {
        SpawnObjectPool();
    }

    protected virtual void SpawnObjectPool() {
        PoolManager.GetInstance.CreatePool(m_PoolInfo.prefab, m_PoolInfo.poolSize);
    }
}
