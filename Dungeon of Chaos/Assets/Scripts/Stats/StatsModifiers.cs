using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
