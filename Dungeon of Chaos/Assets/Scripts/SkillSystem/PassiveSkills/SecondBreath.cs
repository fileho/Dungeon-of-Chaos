using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/SecondBreath")]
public class SecondBreath : IPassiveSkill
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private float duration;
    [SerializeField] private List<ISkillEffect> effects;
    [SerializeField] private SoundSettings sfx = new SoundSettings();

    private bool wasResurrected = false;

    

    public bool ShouldResurrect()
    {
        return !wasResurrected;
    }

    public void Resurrect(Unit unit)
    {
        var clone = Instantiate(vfx, unit.transform);
        clone.GetComponent<VisualEffects>().Init(duration, unit, effects);
        SoundManager.instance.PlaySound(sfx);
        wasResurrected = true;
    }

    public override void Equip(Stats stats)
    {
        wasResurrected = false;
    }

    public override string GetSkillDescription()
    {
        return string.Format(skillData.GetDescription(), effects[0].GetEffectsValues(Character.instance)[0]);
    }

    public override void Unequip(Stats stats)
    {
        return;
    }
}
