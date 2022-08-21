using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/StatsModifiers")]
public class StatsModifiers : ScriptableObject
{
    [SerializeField] private float statsBonus;
    private const float lvlBonus = 0.3f;

    [SerializeField] private float xpMultiplier;
    private const float lvlXPMultiplier = 1.5f;

    [SerializeField] private float lifeEssence;
    private const float essenceLvlMultiplier = 1.15f;
    [SerializeField] private float essenceChance;
    private const float lvlEssenceChanceMultiplier = 1.08f;
    private const int maxLevel = 35;
    [SerializeField] private float staminaEssence;
    [SerializeField] private float manaEssence;

    public float GetStatsBonus()
    {
        return statsBonus;        
    }

    public float GetLvlBonus()
    {
        return lvlBonus;
    }
}
