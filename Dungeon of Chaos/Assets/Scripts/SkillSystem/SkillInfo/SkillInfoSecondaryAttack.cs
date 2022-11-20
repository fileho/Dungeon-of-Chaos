using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoSecondary")]
public class SkillInfoSecondaryAttack : SkillInfo<ISecondaryAttack>
{
    public void Upgrade()
    {
        level++;
    }

    public SkillData GetSkillData()
    { 
        return skills[GetIndex()].GetSkillData();
    }

    public string GetDescription(int inc = 0)
    {
        if (level + inc == 0 || level == maxLevel)
            return "";

        return skills[level -1 + inc].GetDescription();
    }
}
