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
    [SerializeField] private SkillData skillData;

    [SerializeField] private float cooldown;
    [SerializeField] private float manaCost;
    [SerializeField] private float staminaCost;

    [SerializeField] private List<ISkillEffect> effects;

    private float cooldownLeft;

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

    public void Use(Unit unit)
    {
        if (!CanUse(unit.stats))
            return;

        cooldownLeft = cooldown;
        Consume(unit.stats);

        foreach (var e in effects)
        {
            e.Use(unit);
        }
    }

    private void Consume(Stats stats)
    {
        stats.ConsumeMana(manaCost);
        stats.ConsumeStamina(staminaCost);
    }
}
