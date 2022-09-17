using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Projectile")]
public class ProjectileSkill : ISkillEffect
{
    [SerializeField] private float speed;

    [SerializeField] private Projectile prefab;
    [SerializeField] private List<ISkillEffect> effects;

    protected override void ApplyOnPositions(Unit unit, List<Vector2> targetPositions)
    {
        foreach (var targetPos in targetPositions)
        {
            var projectile = Instantiate(prefab, unit.transform.position, Quaternion.identity);
            projectile.Init(effects, unit, speed, targetPos);
        }
    }
}
