using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidPoolSpawner : PoolSpawner {

    public float startDelay;    //  开启陨石波的延迟
    public float spawnWait;     //  单个陨石生成的间隔

    public Vector2 spawnValue;

    public bool activatePoolObjectsOnStart = false;

    #region Unity life cycle
    protected override void Start() {
        base.Start();

        //  开始时开启陨石波
        if (activatePoolObjectsOnStart) {
            StartCoroutine(ActivateAsteroidWave());
        }
    }

    protected void OnEnable() {
        //  事件绑定
        AsteroidBehaviour.AsteroidReuse += SingleReuse; 
    }

    private void OnDisable() {
        //  事件解绑
        AsteroidBehaviour.AsteroidReuse -= SingleReuse;
    }
    #endregion

    protected override void SpawnObjectPool() {
        base.SpawnObjectPool();
    }

    //  绑定在事件: AsteroidBehaviour.AsteroidReuse 上， 该事件在陨石发生可引起销毁的碰撞时触发
    private void SingleReuse() {
        StartCoroutine(ReuseSingleAsteroid(spawnWait));
    }
    
    //  开启陨石波
    IEnumerator ActivateAsteroidWave() {
        yield return new WaitForSeconds(startDelay);
        
        for (int i = 0; i < m_PoolInfo.poolSize; i++) {
            StartCoroutine(ReuseSingleAsteroid(0));
            yield return new WaitForSeconds(spawnWait);
        }
    }

    //  单个陨石的重用
    IEnumerator ReuseSingleAsteroid(float spawnDelay) {
        yield return new WaitForSeconds(spawnDelay);
        Vector2 spawnPosition = new Vector2(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y);
        Quaternion spawnRotation = Quaternion.identity;

        PoolManager.GetInstance.ReuseObject(m_PoolInfo.prefab, spawnPosition, spawnRotation);
    }

    #region TETS
    //  test mode, reuse single asteroid on hitting space
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(ReuseSingleAsteroid(spawnWait));
        }
    }
    #endregion
}
