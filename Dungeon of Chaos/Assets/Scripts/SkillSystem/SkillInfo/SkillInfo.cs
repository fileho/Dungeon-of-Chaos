using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedSkillInfo
{
    public string id;
    public int level;
    public bool unlocked;

    public SavedSkillInfo(string id, int level, bool unlocked)
    {
        this.id = id;
        this.level = level;
        this.unlocked = unlocked;
    }
}

public struct SkillDescription
{
    public string header;
    public string cost;
    public string description;
    public string requirements;

    public SkillDescription(string h="", string c="", string d="", string r="")
    {
        header = h;
        cost = c;
        description = d;
        requirements = r;
    }

    public int GetLongestLength()
    {
        string[] array = new string[] { header, cost, description, requirements };
        return array.Max(w => w.Length);
    }
}

public class SkillInfo<T> : ScriptableObject where T : ISkill
{
    [SerializeField] private new string name;
    [SerializeField] protected List<T> skills;
    [SerializeField] private List<UnlockingRequirements> requirements = new List<UnlockingRequirements>();

    private bool unlocked = false;

    [SerializeField] protected int level = 0;

    [SerializeField] protected int maxLevel;

    public UnlockingRequirements GetUnlockingRequirements()
    {
        if (requirements.Count == 0)
            return new UnlockingRequirements();
        if (level >= requirements.Count)
        {
            requirements.Add(new UnlockingRequirements(requirements[requirements.Count - 1], level));
        }
        return requirements[level];
    }
    public SkillData GetSkillData()
    {
        return skills[GetIndex()].GetSkillData();
    }

    public SkillDescription GetCurrentDescription()
    {
        if (level == 0)
            return new SkillDescription("Locked");
        return new SkillDescription("Current Level", GetCost(), GetDescription());
    }

    public SkillDescription GetNextDescription()
    {
        if (level == maxLevel)
            return new SkillDescription("Max level reached");
        return new SkillDescription("Next Level", GetCost(1), GetDescription(1), GetRequirementsDescription());
    }

    public string GetCost(int inc = 0)
    {
        if (level + inc == 0 || level > maxLevel)
            return "";
        return skills[level - 1 + inc].GetCostDescription();
    }

    public string GetDescription(int inc = 0)
    {
        if (level + inc == 0 || level > maxLevel)
            return "";

        return skills[level - 1 + inc].GetEffectDescription();
    }

    public string GetRequirementsDescription()
    {
        if (requirements.Count == 0 || level == maxLevel)
            return "";
        if (level >= requirements.Count)
        {
            UnlockingRequirements req = GetUnlockingRequirements();
        }
        return requirements[level].GetRequirementsDescription();
    }    

    public string GetId()
    {
        return name;
    }

    public List<T> GetSkills()
    {
        return skills;
    }

    public bool IsUnlocked()
    {
        return unlocked;
    }

    public void Unlock()
    {
        unlocked = true;
    }

    protected int GetIndex()
    {
        int index = level - 1;
        if (level == 0)
            index += 1;
        return index;
    }

    public T GetCurrentSkill()
    {
        return skills[GetIndex()];
    }

    public int GetLevel()
    {
        return level;
    }

    public bool CanUpgrade()
    {
        return level < maxLevel;
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public void ResetLevel()
    {
        level = 0;
    }

    public SavedSkillInfo Save()
    {
        return new SavedSkillInfo(name, level, unlocked);
    }

    public void Load(SavedSkillInfo saved)
    {
        level = saved.level;
        unlocked = saved.unlocked;
    }
}
