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
        if (!skillSystem.IsUnlocked(skillInfo))
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
        time = 0f;
        rightClick = false;
        if (!skillSystem.CanUpgrade(skillInfo))
        {
            Debug.Log("Not enough skill points");
            return;
        }

        skillSystem.Upgrade(skillInfo);
        SetIcon();
        SetLevel();
    }

    public override void SetLevel()
    {
        if (skillInfo.GetLevel() == 0)
            locked.SetActive(true);
        locked.SetActive(false);
        level.text = skillInfo.GetLevel() + "/" + skillInfo.GetMaxLevel();
    }

    public override void SetIcon()
    {
        Sprite icon = skillInfo.GetSkillData().GetIcon();
        GetComponent<Image>().sprite = icon;

        if (skillSystem.IsEquipped(skillInfo))
        {
            Debug.Log("Update icon in activated slots");
        }
    }
}
