using System.Collections.Generic;
using UnityEngine;

public abstract class SkillSlots<T> : MonoBehaviour
{
    private List<SkillSlot> skillSlots = new List<SkillSlot>();

    protected SkillSystem skillSystem;

    public void Init()
    {
        if (skillSystem == null)
            skillSystem = FindObjectOfType<SkillSystem>();
        if (skillSlots.Count != GetAmountOfSlots())
            InitSkillSlots();
        RemoveHighlight();
    }

    protected abstract int GetAmountOfSlots();
    protected abstract SkillInfo<T> GetSkill(int i);
    protected abstract Sprite GetIcon(int i);

    protected abstract SkillSlot[] GetSkillSlots();

    private void InitSkillSlots()
    {
        SkillSlot[] slots = GetSkillSlots();
        for (int i = 0; i < GetAmountOfSlots(); i++)
            skillSlots.Add(null);
        foreach (SkillSlot slot in slots)
        {
            skillSlots[slot.GetIndex()] = slot;
        }
        Redraw();
    }

    public void Highlight()
    {
        foreach (SkillSlot skillSlot in skillSlots)
        {
            skillSlot.Highlight();
        }
    }

    public void RemoveHighlight()
    {
        foreach (SkillSlot skillSlot in skillSlots)
        {
            skillSlot.RemoveHighlight();
        }
    }

    public void Redraw()
    {
        for (int i = 0; i < skillSlots.Count; i++)
        {
            if (GetSkill(i) == null)
            {
                skillSlots[i].SetImage(null);
                continue;
            }
            skillSlots[i].SetImage(GetIcon(i));
        }
    }
}
