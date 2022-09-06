using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoActive")]
public class SkillInfoActive : SkillInfo<IActiveSkill>
{
    public void Upgrade()
    {
        level++;
    }

    public SkillData GetSkillData()
    {
        int index = level - 1;
        if (level == 0)
            index += 1;

        return skills[index].GetSkillData();
    }
}

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoPassive")]
public class SkillInfoPassive : SkillInfo<IPassiveSkill>
{
    public void Upgrade()
    {
        Unequip(Character.instance.stats);
        level++;
        Equip(Character.instance.stats);
    }
    public void Equip(Stats stats)
    {
        skills[level].Equip(stats);
    }

    public void Unequip(Stats stats)
    {
        skills[level].Unequip(stats);
    }

    public SkillData GetSkillData()
    {
        int index = level - 1;
        if (level == 0)
            index += 1;

        return skills[index].GetSkillData();
    }
}


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
