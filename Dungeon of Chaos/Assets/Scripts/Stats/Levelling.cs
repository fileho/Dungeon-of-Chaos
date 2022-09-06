using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Levelling
{
    private int currentXP = 0;
    private int nextLevelXP;
    [Header("Levelling settings")]
    [Tooltip("Only for character")]
    [SerializeField] private int maxLevel;
    [SerializeField] private float baseMultiplier;
    [SerializeField] private int baseXP;
    [Header("Starting level")]
    [SerializeField] private int level = 1;

    [SerializeField] private int statsPoints;

    public void SetNextLevelXP()
    {
        if (level == 1) 
        {
            nextLevelXP = baseXP;
            return;
        }
       nextLevelXP = Mathf.CeilToInt((baseMultiplier - ((level + 1) / (maxLevel+1))) * nextLevelXP);
    }

    public int GetCurrentXP()
    {
        return currentXP;
    }

    public void ModifyCurrentXP(int value)
    {
        currentXP += value;
    }

    public int GetNextLevelXP()
    {
        return nextLevelXP;
    }

    public int GetLevel()
    {
        return level;
    }

    public bool CanLevelUp()
    {
        return level < maxLevel && currentXP >= nextLevelXP;
    }

    public void LevelUp()
    {
        level++;
        statsPoints += GetStatsPointsReward();
        SkillSystem.instance.skillPoints += GetSkillPointsReward();
        currentXP -= nextLevelXP;
        SetNextLevelXP();
    }

    private int GetStatsPointsReward()
    {
        // Can be modified based on level
        return 2;
    }

    private int GetSkillPointsReward()
    {
        // Can be modified based on level
        return 1;
    }   
    
    public bool HasStatsPoints()
    {
        return statsPoints > 0;
    }

    public bool HasSkillPoints()
    {
        return SkillSystem.instance.skillPoints > 0;
    }

    public int GetStatsPoints()
    {
        return statsPoints;
    }

    public void ConsumeStatsPoint()
    {
        statsPoints--;
    }
}
