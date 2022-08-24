using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoActive")]
public class SkillInfoActive : SkillInfo<IActiveSkill>
{
}

[CreateAssetMenu(menuName = "SO/SkillSystem/SkillInfoPassive")]
public class SkillInfoPassive : SkillInfo<IPassiveSkill>
{
    public void Equip(Stats stats)
    {
        skills[level].Equip(stats);
    }
}


public class SkillInfo<T> : ScriptableObject
{
    [SerializeField] protected List<T> skills;

    [SerializeField] protected int level;
}
