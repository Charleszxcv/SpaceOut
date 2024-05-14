using UnityEngine;
using System;

public class Bonus : MonoBehaviour {
    public SkillType m_SkillType;
    public static event Action<SkillType> OnGetSkill;

    public float spawningDelay;

    //  when colliding with another object, if another objct is 'Player', sending command to the 'Player'
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Player") 
        {
            if (OnGetSkill != null) {
                OnGetSkill(m_SkillType);
            }

            if (!LevelController.skillPickedStateDic.ContainsKey(m_SkillType)) {
                LevelController.skillPickedStateDic.Add(m_SkillType, true);
                Debug.Log(string.Format("picked: {0}", m_SkillType));
            }

            Destroy(gameObject);
        }
    }
}
