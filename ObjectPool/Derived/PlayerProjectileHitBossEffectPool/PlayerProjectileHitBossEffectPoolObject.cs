using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileHitBossEffectPoolObject : PoolObject {
    private ParticleSystem m_ParticleSystem;

    private void Awake() {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    public override void OnObjectReuse() {
        base.OnObjectReuse();
        m_ParticleSystem.Play();
    }
}
