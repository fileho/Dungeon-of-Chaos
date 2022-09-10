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
