using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedSkillSlots : MonoBehaviour
{
    private List<SkillSlotActive> skillSlots = new List<SkillSlotActive>();

    private SkillSystem skillSystem;

    private void Init()
    {
        if (skillSystem == null)
            skillSystem = FindObjectOfType<SkillSystem>();
    }

    public void InitSkillSlots()
    {
        Init();
        if (skillSlots.Count == skillSystem.GetActiveSkillSlots())
            return;
        SkillSlotActive[] slots = FindObjectsOfType<SkillSlotActive>();
        for (int i = 0; i < skillSystem.GetActiveSkillSlots(); i++)
            skillSlots.Add(null);
        foreach (SkillSlotActive slot in slots)
        {
            skillSlots[slot.GetIndex()] = slot;
        }
        Redraw();
    }

    public void Highlight()
    {
        foreach (SkillSlotActive skillSlot in skillSlots)
        {
            skillSlot.Highlight();
        }
    }

    public void RemoveHighlight()
    {
        foreach (SkillSlotActive skillSlot in skillSlots)
        {
            skillSlot.RemoveHighlight();
        }
    }

    public void Redraw()
    {
        for (int i = 0; i < skillSlots.Count; i++)
        {
            if (skillSystem.GetSkillInfoActive(i) == null)
            {
                skillSlots[i].SetImage(null);
                continue;
            }
            skillSlots[i].SetImage(skillSystem.GetSkillInfoActive(i).GetSkillData().GetIcon());
        }
    }

}
