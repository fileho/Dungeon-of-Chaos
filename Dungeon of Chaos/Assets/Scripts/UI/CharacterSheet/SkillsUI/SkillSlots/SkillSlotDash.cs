using UnityEngine.EventSystems;

public class SkillSlotDash : SkillSlot
{
    public void Init()
    {
        skillSystem = FindObjectOfType<SkillSystem>();
        if (skillSystem.GetActivatedDash() == null)
        {
            SetImage(null);
            return;
        }
        SetImage(skillSystem.GetActivatedDash().GetSkillData().GetIcon());
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || 
            eventData.pointerDrag.GetComponent<SkillButtonDash>() == null)
            return;
        SoundManager.instance.PlaySound(drop);
        int skillIndex = eventData.pointerDrag.GetComponent<SkillButtonDash>().GetSkillIndex();
        skillSystem.ActivateDash(skillIndex);
        SetImage(skillSystem.GetActivatedDash().GetSkillData().GetIcon());
    }
}
