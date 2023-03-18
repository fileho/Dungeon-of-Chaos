using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Projectile")]
public class ProjectileSkill : ISkillEffect
{
    [SerializeField] private float speed;

    [SerializeField] private Projectile prefab;
    [SerializeField] private List<ISkillEffect> effects;

    [SerializeField] private int amountOfProjectiles = 1;

    private const float coneWidth = 90;

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
            float rotation = amountOfProjectiles == 1 
                ? 0 
                : coneWidth / (amountOfProjectiles - 1);
            float initialAngle = -rotation;
            for (int i = 0; i < amountOfProjectiles; i++)
            {
                float angle = (initialAngle + i * rotation) * Mathf.Deg2Rad;
                Vector3 targetPosition = new Vector3(targetPos.x, targetPos.y, 0);
                //Vector2 dir = Quaternion.AngleAxis(initialAngle + i * rotation, Vector3.forward) * (targetPosition - unit.transform.position).normalized;
                Vector2 dir = Quaternion.Euler(0, 0, angle) * targetPosition;
                var projectile = Instantiate(prefab, unit.transform.position, Quaternion.identity);
                projectile.Init(effects, unit, speed, dir);
            }            
        }
    }
}
