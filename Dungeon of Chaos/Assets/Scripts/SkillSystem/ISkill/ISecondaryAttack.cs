using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Skills/SecondaryAttack")]
public class ISecondaryAttack : IActiveSkill
{
    [SerializeField] private IAttack secondaryAttack;
    [SerializeField] private AttackConfiguration attackConfiguration;

    public override void Use(Unit unit, List<Unit> targets = null, List<Vector2> targetPositions = null)
    { 
        if (!CanUse(unit.stats))
            return;
        if (secondaryAttack.IsAttacking())
            return;

        cooldownLeft = cooldown;
        Consume(unit.stats);
        
        secondaryAttack.Attack();
    }

    public void Init(Unit owner)
    {
        staminaCost = secondaryAttack.GetStaminaCost();
        cooldown = secondaryAttack.GetCoolDownTime();
        secondaryAttack = secondaryAttack.Init(owner, owner.GetComponentInChildren<Weapon>(), attackConfiguration);
    }
}
