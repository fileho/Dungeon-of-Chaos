using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPassiveSkill : ScriptableObject
{
    [SerializeField] private SkillData skillData;




    public abstract void Equip(Stats stats);
    public abstract void Unequip(Stats stats);
}
