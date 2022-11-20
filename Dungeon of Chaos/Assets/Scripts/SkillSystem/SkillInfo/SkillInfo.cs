using System;
using System.Collections;
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

public class SkillInfo<T> : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] protected List<T> skills;

    private bool unlocked = false;

    [SerializeField] protected int level = 0;

    [SerializeField] protected int maxLevel;

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
