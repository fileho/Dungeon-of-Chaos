using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonPassive : SkillButton
{
    [SerializeField] private SkillInfoPassive skillInfo;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!SkillSystem.instance.IsUnlocked(skillInfo))
            return;
        dragDrop.transform.position = eventData.position;
        dragDrop.GetComponent<Image>().sprite = skillInfo.GetSkillData().GetIcon();
        dragDrop.SetActive(true);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnPointerExit");
    }

    public SkillInfoPassive GetSkillInfo()
    {
        return skillInfo;
    }

    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }
}
