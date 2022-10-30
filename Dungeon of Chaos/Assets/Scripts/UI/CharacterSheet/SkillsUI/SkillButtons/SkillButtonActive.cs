using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonActive : SkillButton
{
    [SerializeField] private int skillIndex;
    private ActivatedSkillSlots activatedSkillSlots;
    private SkillInfoActive skillInfo;

    public override void Init()
    {
        base.Init();
        activatedSkillSlots = FindObjectOfType<ActivatedSkillSlots>();
        skillInfo = skillSystem.GetSkillInfoActive(skillIndex);
        if (skillInfo == null)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        SetLevel();
        SetIcon();
        frame.color = Color.red;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || !skillSystem.IsUnlockedActive(skillIndex))
        {
            eventData.pointerDrag = null;
            return;
        }

        dragDrop.transform.position = eventData.position;
        dragDrop.GetComponent<Image>().sprite = skillInfo.GetSkillData().GetIcon();
        dragDrop.SetActive(true);

        activatedSkillSlots.Highlight();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        string ch1 = GetLevelDescription(skillInfo.GetLevel());
        TooltipSystem.instance.Show(skillInfo.GetSkillData().GetName(), "Active Skill", ch1, skillInfo.GetDescription(), 
            GetNextLevelDescription(skillInfo.GetLevel(), skillInfo.GetMaxLevel()), skillInfo.GetDescription(1));
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }

    public override void RightMouseDown()
    {
        if (!skillSystem.CanUpgradeActive(skillIndex))
        {
            TooltipSystem.instance.DisplayMessage("Not enough skill points");
            return;
        }
        base.RightMouseDown();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        activatedSkillSlots.RemoveHighlight();
    }

    public int GetSkillIndex()
    {
        return skillIndex;
    }

    public override void Upgrade() 
    {
        time = 0f;
        rightClick = false;

        skillSystem.UpgradeActive(skillIndex);
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
        locked.SetActive(false);
        if (skillInfo.GetLevel() == 0)
            locked.SetActive(true);        
        level.text = skillInfo.GetLevel() + "/" + skillInfo.GetMaxLevel();
    }
}
