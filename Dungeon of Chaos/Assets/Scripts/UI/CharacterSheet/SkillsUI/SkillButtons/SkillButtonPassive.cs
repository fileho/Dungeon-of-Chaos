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
        {
            transform.parent.gameObject.SetActive(false);
            return;
        }
        SetLevel();
        SetIcon();
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
            eventData.pointerDrag = null;
            return;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.instance.Show(skillInfo.GetSkillData().GetName(), "Passive Skill",
            skillInfo.GetCurrentDescription(), skillInfo.GetNextDescription());
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        return;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.Hide();
    }

    public override void RightMouseDown()
    {
        if (!skillSystem.CanUpgradePassive(skillIndex))
        {
            TooltipSystem.instance.DisplayMessage("Requirements not met");
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
}
