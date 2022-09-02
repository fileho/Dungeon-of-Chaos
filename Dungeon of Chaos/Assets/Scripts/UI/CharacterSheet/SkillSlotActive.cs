using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotActive : SkillSlot
{ 
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        SkillInfoActive skillInfo = eventData.pointerDrag.GetComponent<SkillButtonActive>().GetSkillInfo();
        SkillSystem.instance.Activate(skillInfo, index);
        GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
    }
}
