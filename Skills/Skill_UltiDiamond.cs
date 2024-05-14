using System.Collections;
using UnityEngine;
using System;

[System.Serializable]
public class UltiDiamondInfo {
    public GameObject m_UltiDiamondPrefab;
    public float m_UltiDuration;
}

public class Skill_UltiDiamond : SkillController {
    public UltiDiamondInfo ultiInfo;
    public event Action<UltiDiamondInfo> FireSkill_UltiDiamond;

    protected override void OnEnable() {
        base.OnEnable();
        playerSkillControl.Event_Ulti_Diamond += OnFireSkill;
    }

    protected override void OnDisable() {
        base.OnDisable();
        playerSkillControl.Event_Ulti_Diamond -= OnFireSkill;
    }

    protected override void OnFireSkill() {
        if (!SkillEnabled || this.isCooling) { return; }

        if (FireSkill_UltiDiamond != null) {
            FireSkill_UltiDiamond(ultiInfo);
        }

        Cooling();
    }
}
