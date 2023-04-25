using UnityEngine;

/// <summary>
/// Values used for modifying enemy stats based on level and power type
/// </summary>
[CreateAssetMenu(menuName = "SO/Stats/StatsModifiers")]
public class StatsModifiers : ScriptableObject
{
    [SerializeField] private float statsBonus;
    [SerializeField] private float hpMultiplier;
    private const float lvlBonus = 0.75f;

    public float GetStatsBonus()
    {
        return statsBonus;
    }

    public float GetLvlBonus()
    {
        return lvlBonus;
    }

    public float GetHPMultiplier()
    {
        return hpMultiplier;
    }
}
