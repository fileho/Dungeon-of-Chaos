using UnityEngine;

/// <summary>
/// Passive skill that increases armor
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/Armor")]
public class IncreaseArmor : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.SetArmor(val);
    }
}
