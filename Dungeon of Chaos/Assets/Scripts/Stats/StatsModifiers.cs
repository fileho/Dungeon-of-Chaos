using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Stats/StatsModifiers")]
public class StatsModifiers : ScriptableObject
{
    [SerializeField] private float statsBonus;
    private const float lvlBonus = 0.3f;

    public float GetStatsBonus()
    {
        return statsBonus;        
    }

    public float GetLvlBonus()
    {
        return lvlBonus;
    }
}
