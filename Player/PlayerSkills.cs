using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour {

    [Serializable]
    public class SkillInputInfo {
        public Button btn;
        public KeyCode key;
        public bool isSupported = false;
    }

    public SkillInputInfo rateUp;
    public SkillInputInfo powerUp;
    public SkillInputInfo diamond;
    public SkillInputInfo medic;

    public event Action Event_PowerUp;
    public event Action Event_Ulti_Diamond;
    public event Action Event_Ulti_Medic;
    public event Action Event_RateUp;

    private void OnEnable() {
        //  add btns onclick listeners
        if (rateUp.isSupported) rateUp.btn.onClick.AddListener(FireSkill_RateUp);
        if (powerUp.isSupported) powerUp.btn.onClick.AddListener(FireSkill_PowerUp);
        if (diamond.isSupported) diamond.btn.onClick.AddListener(FireSkill_Ulti_Diamond);
        if (medic.isSupported) medic.btn.onClick.AddListener(FireSkill_Ulti_Medic);
    }

    private void OnDisable() {
        //  remove btns onclick listeners
        if (rateUp.isSupported) rateUp.btn.onClick.RemoveListener(FireSkill_RateUp);
        if (powerUp.isSupported) powerUp.btn.onClick.RemoveListener(FireSkill_PowerUp);
        if (diamond.isSupported) diamond.btn.onClick.RemoveListener(FireSkill_Ulti_Diamond);
        if (medic.isSupported) medic.btn.onClick.RemoveListener(FireSkill_Ulti_Medic);
    }

    private void Update() {
        if (Input.GetKeyDown(rateUp.key)) FireSkill_RateUp();
        if (Input.GetKeyDown(powerUp.key)) FireSkill_PowerUp();
        if (Input.GetKeyDown(diamond.key)) FireSkill_Ulti_Diamond();
        if (Input.GetKeyDown(medic.key)) FireSkill_Ulti_Medic();

        if (Input.GetKey(KeyCode.LeftShift)) {
            if (Input.GetKey(KeyCode.KeypadEnter)) {
                if (rateUp.isSupported) rateUp.btn.GetComponent<Skill_RateUp>().EnableSkill(SkillType.RATE_UP);
                if (powerUp.isSupported) powerUp.btn.GetComponent<Skill_PowerUp>().EnableSkill(SkillType.POWER_UP);
                if (diamond.isSupported) diamond.btn.GetComponent<Skill_UltiDiamond>().EnableSkill(SkillType.ULTI_DIAMOND);
                if (medic.isSupported) medic.btn.GetComponent<Skill_Medic>().EnableSkill(SkillType.ULTI_MEDIC);
            }
        }
    }

    #region Callbacks
    private void FireSkill_PowerUp() {
        if (Event_PowerUp != null) {
            Event_PowerUp();
        }
    }

    private void FireSkill_Ulti_Diamond() {
        if (Event_Ulti_Diamond != null) {
            Event_Ulti_Diamond();
        }
    }

    private void FireSkill_Ulti_Medic() {
        if (Event_Ulti_Medic != null) {
            Event_Ulti_Medic();
        }
    }

    private void FireSkill_RateUp() {
        if (Event_RateUp != null) {
            Event_RateUp();
        }
    }
    #endregion
}
