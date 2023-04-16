using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Skills/Active")]
public class IActiveSkill : ISkill
{
    [SerializeField]
    protected float cooldown;
    [SerializeField]
    protected float manaCost;
    [SerializeField]
    protected float staminaCost;

    [SerializeField]
    protected List<ISkillEffect> effects;

    protected float cooldownLeft;

    private float RecalculateCooldown()
    {
        return cooldown / Character.instance.stats.GetCooldownModifier();
    }

    private float RecalculateManaCost()
    {
        float mCost = manaCost > 0
            ? manaCost + Character.instance.stats.GetManaCostInc()
            : manaCost;
        return mCost * Character.instance.stats.GetManaCostMod();
    }

    private float RecalculateStaminaCost()
    {
        float sCost = staminaCost > 0
            ? staminaCost + Character.instance.stats.GetStaminaCostInc()
            : staminaCost;
        return sCost * Character.instance.stats.GetStaminaCostMod();
    }

    public override string GetEffectDescription()
    {
        List<string> descriptionValues = new List<string>();
        foreach (ISkillEffect effect in effects)
        {
            var d = effect.GetEffectsValues(Character.instance);
            if (d == null)
                continue;
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] != null)
                    descriptionValues.Add(d[i]);
            }
        }
        object[] args = descriptionValues.ToArray();
        string skillDes = string.Format(skillData.GetDescription(), args);

        return skillDes;
    }

    public override string GetCostDescription()
    {
        string mCost = RecalculateManaCost() > 0 ? "<i>Mana Cost:</i> " + RecalculateManaCost().ToString() + " " : "";
        string sCost = RecalculateStaminaCost() > 0 ? "<i>Stamina Cost:</i> " + RecalculateStaminaCost().ToString() + " " : "";
        string cool = RecalculateCooldown() > 0 ? "<i>Cooldown:</i> " + RecalculateCooldown().ToString() + " s" : "";
        return mCost + sCost + cool;
    }

    public bool CanUse(Stats stats)
    {
        if (!stats.HasMana(manaCost))
        {
            InGameUIManager.instance.NotEnoughMana();
            return false;
        }

        if (!stats.HasStamina(staminaCost))
        {
            InGameUIManager.instance.NotEnoughStamina();
            return false;
        }

        return cooldownLeft <= 0;
    }

    public void UpdateCooldown()
    {
        if (cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

    public virtual void Use(Unit unit, List<Unit> targets = null, List<Vector2> targetPositions = null)
    {
        if (!CanUse(unit.stats))
        {
            return;
        }

        cooldownLeft = RecalculateCooldown();
        Consume(unit.stats);

        foreach (var e in effects)
        {
            e.Use(unit, targets, targetPositions);
        }
    }

    public float GetCooldownRatio()
    {
        return cooldownLeft / cooldown;
    }

    protected void Consume(Stats stats)
    {
        stats.ConsumeMana(RecalculateManaCost());
        stats.ConsumeStamina(RecalculateStaminaCost());
    }
}
