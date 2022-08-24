using UnityEngine;

public class ProjecliteSkillDeprecatedDeprecated : SkillDeprecated, IActiveSkillDeprecated
{
    [SerializeField] private Projectile prefab;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
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

        CreateProjectile();
    }

    private void CreateProjectile()
    {
        // Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 
        // Vector2 dir = target - (Vector2)transform.position;

        var p = Instantiate(prefab, transform.position, Quaternion.identity);
        p.SetStats(damage, speed, transform);
    }
}
