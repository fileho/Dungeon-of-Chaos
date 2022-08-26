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
    public abstract void Use(Unit unit);
}
