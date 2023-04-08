using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/Skills/Skills/SecondaryAttack")]
public class ISecondaryAttack : IActiveSkill
{
    [SerializeField] private IAttack attack;
    [SerializeField] private AttackConfiguration attackConfiguration;

    private IAttack secondaryAttack;

    public override string GetEffectDescription()
    {
        float dmg = attackConfiguration.type == SkillEffectType.physical
            ? Character.instance.stats.GetPhysicalDamage() * attackConfiguration.damage
            : Character.instance.stats.GetSpellPower() * attackConfiguration.damage;
        string s = String.Format(skillData.GetDescription(), dmg.ToString(), attackConfiguration.damage.ToString());
        return s;        
    }

    public override void Use(Unit unit, List<Unit> targets = null, List<Vector2> targetPositions = null)
    {
        if (secondaryAttack == null)
            Init(unit);

        if (!CanUse(unit.stats))
        {
            return;
        }
        if (secondaryAttack.IsAttacking() || Character.instance.IsAttacking())
        {
            return;
        }

        cooldownLeft = cooldown;
        Consume(unit.stats);

        secondaryAttack.Attack();
    }

    public void Init(Unit owner)
    {
        secondaryAttack = attack.GetComponent<IAttack>();
        owner.gameObject.transform.Find("Sword").gameObject.AddComponent(secondaryAttack.GetType());
        secondaryAttack = owner.gameObject.GetComponentsInChildren<IAttack>()[1];
        secondaryAttack = secondaryAttack.Init(owner, owner.GetComponentInChildren<Weapon>(), attackConfiguration);
        staminaCost = secondaryAttack.GetStaminaCost();
        cooldown = secondaryAttack.GetCoolDownTime();
    }

    public bool IsAttacking()
    {
        return secondaryAttack != null && secondaryAttack.IsAttacking();
    }

    public void Deactivate()
    {
        Destroy(secondaryAttack);
    }
}
