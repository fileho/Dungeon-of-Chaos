using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill, IActiveSkill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float healAmount;
    [SerializeField] private float manaCost;

    public override bool CanUse()
    {
        return Character.instance.GetMana() >= manaCost;
    }

    public void Use()
    {
        if (!CanUse())
            return;

        Character.instance.ConsumeMana(manaCost);

        Character.instance.RestoreHealth(healAmount);

        CreateEffect();
    }

    private void CreateEffect()
    {
        Instantiate(prefab, transform.position, Quaternion.identity, Character.instance.transform);
    }
}
