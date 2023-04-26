using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List of skill slots for activated skills
/// </summary>
public class ActivatedSkillSlots : SkillSlots<IActiveSkill>
{
    protected override int GetAmountOfSlots()
    {
        return skillSystem.GetActiveSkillSlots();
    }

    protected override SkillInfo<IActiveSkill> GetSkill(int i)
    {
        return skillSystem.GetActivatedSkill(i);
    }

    protected override Sprite GetIcon(int i)
    {
        return skillSystem.GetActivatedSkill(i).GetSkillData().GetIcon();
    }

    protected override SkillSlot[] GetSkillSlots()
    {
        return FindObjectsOfType<SkillSlotActive>();
    }
}
