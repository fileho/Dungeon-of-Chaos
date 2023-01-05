using System;
using UnityEngine;

public abstract class RegenerableStatImprovement : IPassiveSkill
{
    [SerializeField] private float regeneration;
    [SerializeField] private float costMod;

    private float regen;
    private float cost;


    private float CalculateCost(Stats stats)
    {
        return 1 / (costMod * (1 + stats.GetLevel() * 0.2f));
    }

    private float CalculateRegen(Stats stats)
    {
        return regeneration * (1 + stats.GetLevel() * 0.2f);
    }

    public override void Equip(Stats stats)
    {
        regen = CalculateRegen(stats);
        cost = CalculateCost(stats);

        ChangeStat(stats, regen, cost);
    }

    protected abstract void ChangeStat(Stats stats, float reg, float c);

    public override string GetEffectDescription()
    {
        return string.Format(skillData.GetDescription(),
            Math.Round(CalculateRegen(Character.instance.stats),2), Math.Round(CalculateCost(Character.instance.stats),2));
    }

    public override void Unequip(Stats stats)
    {
        ChangeStat(stats, -regen, 1f);
    }
}
