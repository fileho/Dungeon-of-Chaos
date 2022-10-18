using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoDash")]
public class SkillInfoDash : SkillInfo<IDashSkill>
{
    public void Upgrade()
    {
        level++;
    }

    public SkillData GetSkillData()
    { 
        return skills[GetIndex()].GetSkillData();
    }
}
