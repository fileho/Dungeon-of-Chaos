using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Projectile")]
public class ProjectileSkill : ISkillEffect
{
    [SerializeField] private float speed;

    [SerializeField] private Projectile prefab;
    [SerializeField] private List<ISkillEffect> effects;

    [SerializeField] private int amountOfProjectiles = 1;

    private const float coneWidth = 60f;

    public override string[] GetEffectsValues(Unit owner)
    {
        List<string> descriptionValues = new List<string>();
        if (amountOfProjectiles > 1)
            descriptionValues.Add(amountOfProjectiles.ToString());
        foreach (ISkillEffect effect in effects)
        {
            var d = effect.GetEffectsValues(owner);
            if (d == null)
                continue;
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] != null)
                    descriptionValues.Add(d[i]);
            }
        }

        return descriptionValues.ToArray();
    }

    protected override void ApplyOnPositions(Unit unit, List<Vector2> targetPositions)
    {
        foreach (var targetPos in targetPositions)
        {
            float rotation = coneWidth / (amountOfProjectiles - 1);
            float initialAngle = amountOfProjectiles == 1
                ? 0
                : rotation;
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                Vector2 dir = Quaternion.AngleAxis(initialAngle + i*rotation, Vector3.forward) * targetPos;
                var projectile = Instantiate(prefab, unit.transform.position, Quaternion.identity);
                projectile.Init(effects, unit, speed, targetPos);
            }            
        }
    }
}
