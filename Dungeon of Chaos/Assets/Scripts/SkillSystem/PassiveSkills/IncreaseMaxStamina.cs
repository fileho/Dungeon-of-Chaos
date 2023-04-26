using UnityEngine;

/// <summary>
/// Passive skill that increases max stamina
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/Stamina")]
public class IncreaseMaxStamina : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeMaxStamina(val);
    }
}
