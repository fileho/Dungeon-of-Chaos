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

    public virtual string GetDescription()
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

        return skillDes + "\n" + "\n" + GetStaticDescription();
    }   

    protected string GetStaticDescription()
    {
        string mCost = manaCost > 0
            ? "Mana Cost: " + manaCost.ToString() + "\n"
            : "";
        string sCost = staminaCost > 0
            ? "Stamina Cost: " + staminaCost.ToString() + "\n"
            : "";
        string cool = cooldown > 0
            ? "Cooldown: " + cooldown.ToString() + " seconds"
            : "";
        return mCost + sCost + cool;
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
