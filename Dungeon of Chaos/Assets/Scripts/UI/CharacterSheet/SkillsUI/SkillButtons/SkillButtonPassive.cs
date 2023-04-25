using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Visualization of passive skill in UI
/// </summary>
public class SkillButtonPassive : SkillButton
{
    [Tooltip("Index of the skill in skill system")]
    [SerializeField] private int skillIndex;
    private SkillInfoPassive skillInfo;

    public override void Init()
    {
        base.Init();
        skillInfo = skillSystem.GetSkillInfoPassive(skillIndex);
        if (skillInfo == null)
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        SetLevel();
        SetIcon();
        SetRequirementsOverlay();
        SetHighlightOverlay();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
            eventData.pointerDrag = null;
            return;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySound(hover);
        if (TooltipSystem.instance != null)
            TooltipSystem.instance.Show(skillInfo.GetSkillData().GetName(), "Passive Skill", "No further actions required",
            skillInfo.GetCurrentDescription(), skillInfo.GetNextDescription());
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        return;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (TooltipSystem.instance != null)
            TooltipSystem.instance.HideSkillTooltip();
    }

    public override void RightMouseDown()
    {
        if (!skillSystem.CanUpgradePassive(skillIndex))
        {
            if (TooltipSystem.instance != null)
                TooltipSystem.instance.DisplayMessage("Requirements not met");
            SoundManager.instance.PlaySound(requirements);
            return;
        }
        base.RightMouseDown();
    }

    public int GetSkillIndex()
    {
        return skillIndex;
    }

    public override void Upgrade()
    {
        time = 0f;
        rightClick = false;

        skillSystem.UpgradePassive(skillIndex);
        SetIcon();
        SetLevel();
    }

    public override void SetLevel()
    {
        lockObj.SetActive(false);
        if (skillInfo.GetLevel() == 0)
        {
            lockObj.SetActive(true);
            lockObj.GetComponent<Image>().fillAmount = 1;
        }
        level.text = skillInfo.GetLevel() + "/" + skillInfo.GetMaxLevel();
    }

    public override void SetIcon()
    {
        Sprite icon = skillInfo.GetSkillData().GetIcon();
        GetComponent<Image>().sprite = icon;
    }

    public override void SetRequirementsOverlay()
    {
        requirementsNotMet.SetActive(!skillSystem.CanUnlock(skillInfo) && !skillInfo.IsUnlocked());
    }

    public override void SetHighlightOverlay()
    {
        highlight.SetActive(skillSystem.CanUnlock(skillInfo) && skillInfo.IsUnlocked() && skillInfo.CanUpgrade());
    }
}
