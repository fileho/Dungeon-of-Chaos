using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Projectile")]
public class ProjectileSkill : ISkillEffect
{
    [SerializeField] private float speed;

    [SerializeField] private Projectile prefab;
    [SerializeField] private List<ISkillEffect> effects;

    public override string[] GetEffectsValues(Unit owner)
    {
        List<string> descriptionValues = new List<string>();
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
            var projectile = Instantiate(prefab, unit.transform.position, Quaternion.identity);
            projectile.Init(effects, unit, speed, targetPos);
        }
    }
}
