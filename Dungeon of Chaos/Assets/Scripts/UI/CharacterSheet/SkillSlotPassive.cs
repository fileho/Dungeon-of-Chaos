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
        SkillInfoPassive skillInfo = eventData.pointerDrag.GetComponent<SkillButtonPassive>().GetSkillInfo();
        SkillSystem.instance.Equip(skillInfo, index);
        GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
    }
}

