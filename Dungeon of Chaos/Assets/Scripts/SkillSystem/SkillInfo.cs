using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SkillInfo<T> : ScriptableObject
{
    [SerializeField] protected List<T> skills;

    protected int level = 0;

    [SerializeField] protected int maxLevel;

    public List<T> GetSkills()
    {
        return skills;
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
}
