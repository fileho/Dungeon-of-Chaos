using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonActive : SkillButton
{
    [SerializeField] private SkillInfoActive skillInfo;
    SkillSlotActive[] skillSlots;

    protected override void Start()
    {
        base.Start();
        skillSlots = FindObjectsOfType<SkillSlotActive>();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || !skillSystem.IsUnlocked(skillInfo))
        {
            eventData.pointerDrag = null;
            return;
        }

        dragDrop.transform.position = eventData.position;
        dragDrop.GetComponent<Image>().sprite = skillInfo.GetSkillData().GetIcon();
        dragDrop.SetActive(true);

        foreach (SkillSlotActive skillSlot in skillSlots)
            skillSlot.Highlight();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.instance.Show(skillInfo.GetSkillData().GetDescription(), skillInfo.GetSkillData().GetName(), "Active Skill");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }

    public override void RightMouseDown()
    {
        if (!skillSystem.CanUpgrade(skillInfo))
        {
            //Debug.Log("Not enough skill points");
            TooltipSystem.instance.DisplayMessage("Not enough skill points");
            return;
        }
        base.RightMouseDown();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        foreach (SkillSlotActive skillSlot in skillSlots)
            skillSlot.RemoveHighlight();
    }
    public SkillInfoActive GetSkillInfo()
    {
        return skillInfo;
    }

    public override void Upgrade() 
    {
        time = 0f;
        rightClick = false;

        skillSystem.Upgrade(skillInfo);
        SetIcon();
        SetLevel();
    }

    public override void SetIcon()
    {
        Sprite icon = skillInfo.GetSkillData().GetIcon();
        GetComponent<Image>().sprite = icon;

        if (skillSystem.IsActivated(skillInfo))
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
