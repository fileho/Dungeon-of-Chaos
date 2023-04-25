using UnityEngine;

/// <summary>
/// Constants used for transforming primary stats into secondary (in-game) stats
/// </summary>
[CreateAssetMenu(menuName = "SO/Stats/StatsMultipliers")]
public class PrimaryToSecondary : ScriptableObject
{
    [Header("Primary to Secondary Multipliers")]
    public float damageMultiplier;
    public float spellPowerMultiplier;
    public float hpLinearMultiplier;
    public float staminaMultiplier;
    public float manaLinearMultiplier;
    public float hpExp;
    public float manaExp;
    public float hpExpMultiplier;
    public float manaExpMultiplier;
}
