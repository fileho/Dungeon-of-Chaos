using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Reflection;
using UnityEngine;


[System.Serializable]
public class SkillData
{
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;

    public Sprite GetIcon()
    {
        return icon;
    }

    public string GetName()
    {
        return name;
    }

    public string GetDescription()
    {
        return description;
    }
}

[CreateAssetMenu(menuName = "SO/Skills/Skills/Active")]
public class IActiveSkill : ScriptableObject
{
    [SerializeField] protected SkillData skillData;

    [SerializeField] protected float cooldown;
    [SerializeField] protected float manaCost;
    [SerializeField] protected float staminaCost;

    [SerializeField] protected List<ISkillEffect> effects;

    protected float cooldownLeft;

    public SkillData GetSkillData()
    {
        return skillData;
    }

    public bool CanUse(Stats stats)
    {
        return cooldownLeft <= 0 && stats.HasMana(manaCost) && stats.HasStamina(staminaCost);
    }
 
    public void UpdateCooldown()
    {
        if (cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

    public virtual void Use(Unit unit, List<Unit> targets = null, List<Vector2> targetPositions = null)
    {
        if (!CanUse(unit.stats))
            return;

        cooldownLeft = cooldown;
        Consume(unit.stats);

        foreach (var e in effects)
        {
            e.Use(unit, targets, targetPositions);
        }
    }

    protected void Consume(Stats stats)
    {
        stats.ConsumeMana(manaCost);
        stats.ConsumeStamina(staminaCost);
    }
}
