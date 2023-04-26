using UnityEngine;

/// <summary>
/// Passive skill that increases mana regeneration and decreases mana cost
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/ManaImprovement")]
public class ManaImprovement : RegenerableStatImprovement
{
    protected override void ChangeStat(Stats stats, float reg, float c)
    {
        stats.ChangeManaRegen(reg);
        stats.SetManaCostMod(c);
    }
}
