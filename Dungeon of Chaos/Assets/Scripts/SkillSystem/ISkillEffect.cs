using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillEffectType
{
    spell,
    physical
}
public abstract class ISkillEffect : ScriptableObject
{
    [SerializeReference] protected ITarget target;
   
    public abstract void Use(Unit unit);
}
