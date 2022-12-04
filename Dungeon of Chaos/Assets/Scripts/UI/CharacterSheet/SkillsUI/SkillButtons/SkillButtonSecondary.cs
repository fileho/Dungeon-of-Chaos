using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonSecondary : SkillButton
{
    [SerializeField] private int skillIndex;
    private SkillSlotSecondary skillSlot;
    private SkillInfoSecondaryAttack skillInfo;

    public override void Init()
    {
        base.Init();
        skillSlot = FindObjectOfType<SkillSlotSecondary>();
        skillInfo = skillSystem.GetSkillInfoSecondary(skillIndex);
        if (skillInfo == null)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        SetLevel();
        SetIcon();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || !skillSystem.IsUnlockedSecondary(skillIndex))
        {
            eventData.pointerDrag = null;
            return;
        }

        dragDrop.transform.position = eventData.position;
        dragDrop.GetComponent<Image>().sprite = skillInfo.GetSkillData().GetIcon();
        dragDrop.SetActive(true);

        skillSlot.Highlight();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.instance.Show(skillInfo.GetSkillData().GetName(), "Secondary Attack", skillInfo.GetCurrentDescription(), skillInfo.GetNextDescription());
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }

    public override void RightMouseDown()
    {
        if (!skillSystem.CanUpgradeSecondaryAttack(skillIndex))
        {
            TooltipSystem.instance.DisplayMessage("Requirements not met");
            return;
        }
        base.RightMouseDown();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        skillSlot.RemoveHighlight();
    }

    public int GetSkillIndex()
    {
        return skillIndex;
    }

    public override void Upgrade() 
    {
        time = 0f;
        rightClick = false;

        skillSystem.UpgradeSecondary(skillIndex);
        SetIcon();
        SetLevel();
    }

    public override void SetIcon()
    {
        Sprite icon = skillInfo.GetSkillData().GetIcon();
        GetComponent<Image>().sprite = icon;
    }

    public override void SetLevel()
    {
        @lock.SetActive(false);
        if (skillInfo.GetLevel() == 0)
            @lock.SetActive(true);        
        level.text = skillInfo.GetLevel() + "/" + skillInfo.GetMaxLevel();
    }
}
