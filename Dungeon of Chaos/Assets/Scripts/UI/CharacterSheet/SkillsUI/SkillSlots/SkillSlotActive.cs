using UnityEngine.EventSystems;

public class SkillSlotActive : SkillSlot
{
    private ActivatedSkillSlots activatedSkillSlots;

    protected override void Start()
    {
        base.Start();
        activatedSkillSlots = FindObjectOfType<ActivatedSkillSlots>();
    }
    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null || 
            eventData.pointerDrag.GetComponent<SkillButtonActive>() == null)
            return;
        int skillIndex = eventData.pointerDrag.GetComponent<SkillButtonActive>().GetSkillIndex();
        skillSystem.Activate(skillIndex, index);
        activatedSkillSlots.Redraw();
    }
}
