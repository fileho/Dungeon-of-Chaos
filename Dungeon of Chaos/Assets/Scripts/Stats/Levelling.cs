using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Levelling
{
    [System.Serializable]
    public class SavedLevelling
    {
        public int xp;
        public int level;
        public int statsPoints;
        public int skillPoints;

        public SavedLevelling(int xp, int level, int statsPoints, int skillPoints)
        {
            this.xp = xp;
            this.level = level;
            this.statsPoints = statsPoints;
            this.skillPoints = skillPoints;
        }
    }

    private int currentXP = 100;
    private int nextLevelXP;
    [Header("Levelling settings")]
    [Tooltip("Only for character")]
    [SerializeField]
    private int maxLevel;
    [SerializeField]
    private float baseMultiplier;
    [SerializeField]
    private int baseXP;
    [Header("Starting level")]
    [SerializeField]
    private int level = 1;
    [SerializeField]
    private int statsPoints;
    public int skillPoints;

    public void SetNextLevelXP()
    {
        nextLevelXP = GetXPValue(level);
    }

    private int GetXPValue(int lvl)
    {
        int value = baseXP;

        if (lvl == 1)
            return value;

        for (int i = 1; i < lvl; i++)
        {
            value = Mathf.CeilToInt((baseMultiplier - (float)(i + 1) / (maxLevel * 2 + 1)) * value);
        }

        return value;
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
        skillPoints += GetSkillPointsReward();
        currentXP -= nextLevelXP;
        SetNextLevelXP();
        UpdateLevellingUI();
    }

    public void UpdateLevellingUI()
    {
        StatsOverview.instance.SetLevel(level);
        StatsOverview.instance.SetXP(currentXP);
        StatsOverview.instance.SetNextLevelXP(nextLevelXP);
        StatsOverview.instance.SetStatsPoints(statsPoints);
        StatsOverview.instance.UpdateSkillPoints();
        StatsOverview.instance.ShowLevelUpButton(CanLevelUp());
        StatsOverview.instance.ShowStatsIncreaseButtons(HasStatsPoints());
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
        return skillPoints > 0;
    }

    public void ConsumeStatsPoint()
    {
        Debug.Log("Stats Points: " + statsPoints);
        statsPoints--;
        StatsOverview.instance.SetStatsPoints(statsPoints);
        StatsOverview.instance.ShowStatsIncreaseButtons(HasStatsPoints());
    }

    public void Load(SavedLevelling saved)
    {
        currentXP = saved.xp;
        level = saved.level;
        statsPoints = saved.statsPoints;
        skillPoints = saved.skillPoints;
    }

    public SavedLevelling Save()
    {
        return new SavedLevelling(currentXP, level, statsPoints, skillPoints);
    }
}
