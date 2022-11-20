using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Skills/Passive")]
public abstract class IPassiveSkill : ISkill
{
    public abstract void Equip(Stats stats);
    public abstract void Unequip(Stats stats);

    public override string GetCostDescription()
    {
        return "";
    }
}
