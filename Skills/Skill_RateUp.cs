using System.Collections;
using UnityEngine;
using System;

public class Skill_RateUp : SkillController {

    public event Action<int> FireSkill_RateUp;
    public int maxSkillLevel;

    protected override void Start() {
        base.Start();
        curSkillLevel = 1;
    }

    protected override void OnEnable() {
        base.OnEnable();
        playerSkillControl.Event_RateUp += OnFireSkill;
    }

    protected override void OnDisable() {
        base.OnDisable();
        playerSkillControl.Event_RateUp -= OnFireSkill;
    }

    protected override void OnFireSkill() {
        if (!SkillEnabled || this.isCooling || ++curSkillLevel > maxSkillLevel) { return; }

        //  do something on firing skill
        if (FireSkill_RateUp != null) {
            FireSkill_RateUp(this.curSkillLevel);
        }

        //  cool down the skill
        Cooling();
    }
}
