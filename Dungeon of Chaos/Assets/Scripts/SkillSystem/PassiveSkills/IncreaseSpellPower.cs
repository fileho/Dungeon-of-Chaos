using UnityEngine;

/// <summary>
/// Passive skill that increases spell power
/// </summary>
[CreateAssetMenu(menuName = "SO/Skills/PassiveSkills/SpellPower")]
public class IncreaseSpellPower : IncreaseStat
{
    protected override void ChangeStat(Stats stats, float val)
    {
        stats.ChangeSpellPower(val);
    }
}
