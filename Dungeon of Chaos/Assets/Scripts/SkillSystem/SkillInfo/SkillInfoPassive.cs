using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoPassive")]
public class SkillInfoPassive : SkillInfo<IPassiveSkill>
{
    public void Upgrade()
    {
        Unequip(Character.instance.stats);
        level++;
        Equip(Character.instance.stats);
    }
    public void Equip(Stats stats)
    {
        skills[level].Equip(stats);
    }

    public void Unequip(Stats stats)
    {
        skills[level].Unequip(stats);
    }

    public SkillData GetSkillData()
    {
        return skills[GetIndex()].GetSkillData();
    }

    public string GetDescription(int inc = 0)
    {
        if (level + inc == 0 || level == maxLevel)
            return "";

        return skills[level - 1 + inc].GetDescription();
    }
}
