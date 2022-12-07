using UnityEngine.EventSystems;

public class SkillSlotSecondary : SkillSlot
{
    public void Init()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
        if (skillSystem.GetActivatedSecondary() == null)
        {
            SetImage(null);
            return;
        }
        SetImage(skillSystem.GetActivatedSecondary().GetSkillData().GetIcon());
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || 
            eventData.pointerDrag.GetComponent<SkillButtonSecondary>() == null)
            return;
        SoundManager.instance.PlaySound(drop);
        int skillIndex = eventData.pointerDrag.GetComponent<SkillButtonSecondary>().GetSkillIndex();
        skillSystem.ActivateSecondaryAttack(skillIndex);
        SetImage(skillSystem.GetActivatedSecondary().GetSkillData().GetIcon());
    }
}
