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
        int index = level - 1;
        if (level == 0)
            index += 1;

        return skills[index].GetSkillData();
    }
}
