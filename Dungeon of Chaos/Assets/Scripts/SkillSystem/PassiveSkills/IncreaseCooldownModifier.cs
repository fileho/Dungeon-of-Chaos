using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/CooldownModifier")]
public class IncreaseCooldownModifier : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeCooldownModifier(val);
    }

    public override string GetSkillDescription()
    {
        value = amount * (1 + Character.instance.stats.GetLevel() * 0.2f);
        
        return string.Format(skillData.GetDescription(), (1+Math.Round(value,2)).ToString());
    }
}
