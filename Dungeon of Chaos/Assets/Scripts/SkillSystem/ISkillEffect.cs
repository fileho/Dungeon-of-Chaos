using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISkillEffect : ScriptableObject
{
    public abstract void Use(Unit unit);
}
