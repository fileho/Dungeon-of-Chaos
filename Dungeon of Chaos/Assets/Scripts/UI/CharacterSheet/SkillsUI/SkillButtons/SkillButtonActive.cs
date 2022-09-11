using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonActive : SkillButton
{
    [SerializeField] private SkillInfoActive skillInfo;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || !SkillSystem.instance.IsUnlocked(skillInfo))
            return;
        dragDrop.transform.position = eventData.position;
        dragDrop.GetComponent<Image>().sprite = GetComponent<Image>().sprite; 
        //dragDrop.GetComponent<Image>().sprite = skillInfo.GetSkillData().GetIcon();
        dragDrop.SetActive(true);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.instance.Show(skillInfo.GetSkillData().GetDescription(), skillInfo.GetSkillData().GetName(), "Active Skill");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }

    public SkillInfoActive GetSkillInfo()
    {
        return skillInfo;
    }

    public override void Upgrade() 
    {
        time = 0f;
        rightClick = false;
        if (!SkillSystem.instance.CanUpgrade(skillInfo))
        {
            Debug.Log("Not enough skill points");
            return;
        }

        SkillSystem.instance.Upgrade(skillInfo);
        SetIcon();
        SetLevel();
    }

    public override void SetIcon()
    {
        Sprite icon = skillInfo.GetSkillData().GetIcon();
        GetComponent<Image>().sprite = icon;

        if (SkillSystem.instance.GetActivatedSkills().Contains(skillInfo))
        {
            Debug.Log("Update icon in activated slots");
        }
    }

    public override void SetLevel()
    {
        locked.SetActive(false);
        if (skillInfo.GetLevel() == 0)
            locked.SetActive(true);        
        level.text = skillInfo.GetLevel() + "/" + skillInfo.GetMaxLevel();
    }
}
