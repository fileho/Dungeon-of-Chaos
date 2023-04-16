using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/IncreaseXP")]
public class IncreaseXPIncome : IncreaseStat
{
    public override void Equip(Stats stats)
    {
        value = amount;
        ChangeStat(stats, value);
    }

    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeXPModifier(val);
    }

    public override string GetEffectDescription()
    {
        value = amount;
        return string.Format(skillData.GetDescription(), Math.Round(value, 2).ToString());
    }
}
