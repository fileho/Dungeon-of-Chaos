using System.Collections;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Reflection;
using UnityEngine;

[System.Serializable]
struct SkillData
{
    private string name;
    private string description;

    private Sprite icon;
}

[CreateAssetMenu(menuName = "SO/Skills/Active")]
public class IActiveSkill : ScriptableObject
{
    [SerializeField] private SkillData skillData;

    [SerializeField] private float cooldown;
    [SerializeField] private float manaCost;
    [SerializeField] private float staminaCost;

    [SerializeField] private List<ISkillEffect> effects;


    public bool CanUse(Stats stats)
    {
        return true;
    }

    public void UpdateCooldown()
    {
        //TODO
    }

    public void Use(Unit unit)
    {
        if (!CanUse(unit.stats))
            return;

        Consume(unit.stats);

        foreach (var e in effects)
        {
            e.Use(unit);
        }
    }

    private void Consume(Stats stats)
    {
        throw new System.NotImplementedException();
    }
}
