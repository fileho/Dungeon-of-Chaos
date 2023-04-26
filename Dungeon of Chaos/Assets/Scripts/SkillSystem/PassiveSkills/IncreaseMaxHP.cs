using UnityEngine;

/// <summary>
/// Passive skill that increases max HP
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/MaxHP")]
public class IncreaseMaxHP : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeMaxHealth(val);
    }
}
