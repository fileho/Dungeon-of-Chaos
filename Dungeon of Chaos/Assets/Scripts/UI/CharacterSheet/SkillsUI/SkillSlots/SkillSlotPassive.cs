using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotPassive : SkillSlot
{
    private EquippedSkillSlots equippedSkillSlots;

    protected override void Start()
    {
        base.Start();
        equippedSkillSlots = FindObjectOfType<EquippedSkillSlots>();
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || 
            eventData.pointerDrag.GetComponent<SkillButtonPassive>() == null)
            return;
        int skillIndex = eventData.pointerDrag.GetComponent<SkillButtonPassive>().GetSkillIndex();
        skillSystem.Equip(skillIndex, index);
        equippedSkillSlots.Redraw();
    }
}

