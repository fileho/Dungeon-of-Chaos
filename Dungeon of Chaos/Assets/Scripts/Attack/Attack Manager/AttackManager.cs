using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Selects the attack to use by an enemy
/// </summary>
public class AttackManager : MonoBehaviour
{
    List<IAttack> attacks;
    private float minimumAttackRange = 0;

    public void Start()
    {
        attacks = GetComponentsInChildren<IAttack>().ToList();
        minimumAttackRange = attacks.Min(x => x.GetAttackRange());
        List<IAttack> sortedAttacks = attacks.OrderByDescending(x => GetAttacKWeight(x)).ToList();
        attacks = sortedAttacks;
    }

    private float GetAttacKWeight(IAttack attack)
    {
        return attack.GetAttackRangeWeighted() + attack.GetDamageWeighted() - attack.GetCost();
    }

    public float GetMinimumAttackRange()
    {
        return minimumAttackRange;
    }

    public IAttack GetBestAvailableAttack()
    {
        for (int i = 0; i < attacks.Count; ++i)
        {
            if (attacks[i].CanAttack())
                return attacks[i];
        }
        return null;
    }
}
