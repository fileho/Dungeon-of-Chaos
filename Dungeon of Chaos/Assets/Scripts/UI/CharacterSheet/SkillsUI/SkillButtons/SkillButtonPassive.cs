using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButtonPassive : SkillButton
{
    [SerializeField] private int skillIndex;
    private SkillInfoPassive skillInfo;

    public override void Init()
    {
        base.Init();
        skillInfo = skillSystem.GetSkillInfoPassive(skillIndex);
        if (skillInfo == null)
            enabled = false;
        frame.color = Color.blue;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (!skillSystem.IsUnlockedPassive(skillIndex))
            return;
        dragDrop.transform.position = eventData.position;
        dragDrop.GetComponent<Image>().sprite = skillInfo.GetSkillData().GetIcon();
        dragDrop.SetActive(true);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
    }

    public int GetSkillIndex()
    {
        return skillIndex;
    }

    public override void Upgrade()
    {
        time = 0f;
        rightClick = false;
        if (!skillSystem.CanUpgradePassive(skillIndex))
        {
            Debug.Log("Not enough skill points");
            return;
        }

        skillSystem.UpgradePassive(skillIndex);
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

        if (skillSystem.IsEquipped(skillIndex))
        {
            Debug.Log("Update icon in activated slots");
        }
    }
}
