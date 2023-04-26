using UnityEngine;

/// <summary>
/// Passive skill that increases mana regeneration
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/ManaRegen")]
public class IncreaseManaRegen : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeManaRegen(val);
    }
}
