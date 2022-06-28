using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecliteSkill : Skill, IActiveSkill
{
    [SerializeField] private Projectile prefab;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
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

        CreateProjectile();
    }

    private void CreateProjectile()
    {
        Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 dir = target - (Vector2)transform.position;

        var p = Instantiate(prefab, transform.position, Quaternion.identity);
        p.SetStats(damage, speed, transform);
    }
}
