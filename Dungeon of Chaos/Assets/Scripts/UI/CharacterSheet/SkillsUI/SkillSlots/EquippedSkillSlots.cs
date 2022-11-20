using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedSkillSlots : SkillSlots<IPassiveSkill>
{
    protected override int GetAmountOfSlots()
    {
        return skillSystem.GetActiveSkillSlots();
    }

    protected override SkillInfo<IPassiveSkill> GetSkill(int i)
    {
        return skillSystem.GetEquippedSkill(i);
    }

    protected override Sprite GetIcon(int i)
    {
        return skillSystem.GetEquippedSkill(i).GetSkillData().GetIcon();
    }

    protected override SkillSlot[] GetSkillSlots()
    {
        return FindObjectsOfType<SkillSlotPassive>();
    }
}
