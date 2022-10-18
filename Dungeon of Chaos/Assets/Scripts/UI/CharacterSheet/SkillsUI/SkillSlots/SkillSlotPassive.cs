using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotPassive : SkillSlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        int skillIndex = eventData.pointerDrag.GetComponent<SkillButtonPassive>().GetSkillIndex();
        skillSystem.Equip(skillIndex, index);
    }
}

