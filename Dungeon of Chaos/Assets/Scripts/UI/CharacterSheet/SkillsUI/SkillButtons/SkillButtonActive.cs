using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Visualization of active skill in UI
/// </summary>
public class SkillButtonActive : SkillButton
{
    [Tooltip("Index of the skill in skill system")]
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
        SetRequirementsOverlay();
        SetHighlightOverlay();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right || 
            !skillSystem.IsUnlockedActive(skillIndex))
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
        SoundManager.instance.PlaySound(hover);
        if (TooltipSystem.instance != null)
            TooltipSystem.instance.Show(skillInfo.GetSkillData().GetName(), "Active Skill", skillSystem.GetActiveStatusDescription(skillIndex),
            skillInfo.GetCurrentDescription(), skillInfo.GetNextDescription());
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (TooltipSystem.instance != null)
            TooltipSystem.instance.HideSkillTooltip();
    }

    public override void RightMouseDown()
    {
        if (!skillSystem.CanUpgradeActive(skillIndex))
        {
            if (TooltipSystem.instance != null)
                TooltipSystem.instance.DisplayMessage("Requirements not met");
            SoundManager.instance.PlaySound(requirements);
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
        lockObj.SetActive(false);
        if (skillInfo.GetLevel() == 0)
        {
            lockObj.SetActive(true);
            lockObj.GetComponent<Image>().fillAmount = 1;
        }
        level.text = skillInfo.GetLevel() + "/" + skillInfo.GetMaxLevel();
    }

    public override void SetRequirementsOverlay()
    {
        requirementsNotMet.SetActive(!skillSystem.CanUnlock(skillInfo) && !skillInfo.IsUnlocked());
    }

    public override void SetHighlightOverlay()
    {
        highlight.SetActive(skillInfo.IsUnlocked() && skillSystem.CanUnlock(skillInfo) && skillInfo.CanUpgrade());
    }
}
