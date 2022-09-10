using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoActive")]
public class SkillInfoActive : SkillInfo<IActiveSkill>
{
    public void Upgrade()
    {
        level++;
    }

    public SkillData GetSkillData()
    {
        int index = level - 1;
        if (level == 0)
            index += 1;

        return skills[index].GetSkillData();
    }
}
