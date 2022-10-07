using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static SoundCategories;

public class AttackManager : MonoBehaviour
{
    List<IAttack> attacks;
    private float minimumAttackRange = 0;

    public void Init() {
        attacks = GetComponentsInChildren<IAttack>().ToList();
        List<IAttack> rangeSortedAttack = attacks.OrderBy(x => x.GetAttackRange()).ToList();
        minimumAttackRange = rangeSortedAttack[0].GetAttackRange();
        List<IAttack> sortedAttacks = attacks.OrderByDescending(x => GetAttacKWeight(x)).ToList();
        attacks = sortedAttacks;
    }


    private float GetAttacKWeight(IAttack attack) {
        // TODO: add weights for each factor
        // weightRange * attack.GetAttackRange()
        // weightDamage * attack.GetDamage()
        // weightStamina * attack.GetStaminaCost()
        return attack.GetAttackRange() + attack.GetDamage() - attack.GetStaminaCost();
    }


    public float GetMinimumAttackRange() {
        return minimumAttackRange;
    }

    public IAttack GetBestAvailableAttack() {
        for (int i = 0; i < attacks.Count; ++i) {
            if (attacks[i].CanAttack())
                return attacks[i];
        }
        return null;
    }
}
