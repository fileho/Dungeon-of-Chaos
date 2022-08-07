using UnityEngine;

public class HealSkill : Skill, IActiveSkill
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float healAmount;
    [SerializeField] private float manaCost;

    public override bool CanUse()
    {
        return Character.instance.stats.HasMana(manaCost);
    }

    public void Use()
    {
        if (!CanUse())
            return;

        Character.instance.stats.ConsumeMana(manaCost);
        Character.instance.stats.RegenerateHealth(healAmount);

        CreateEffect();
    }

    private void CreateEffect()
    {
        Instantiate(prefab, transform.position, Quaternion.identity, Character.instance.transform);
    }
}
