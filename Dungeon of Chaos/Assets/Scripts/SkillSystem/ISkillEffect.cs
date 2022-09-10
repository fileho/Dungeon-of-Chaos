using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISkillEffect : ScriptableObject
{
    public enum SkillEffectType
    {
        spell,
        physical
    }

    [SerializeReference] protected ITarget target;
   
    public abstract void Use(Unit unit);
}
