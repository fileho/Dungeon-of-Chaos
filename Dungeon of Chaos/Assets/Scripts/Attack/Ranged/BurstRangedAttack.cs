using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstRangedAttack : RangedAttack
{
    protected int projectileNumber = 5;

    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        BurstRangedAttackConfiguration _attackConfiguration = attackConfiguration as BurstRangedAttackConfiguration;
        projectileNumber = _attackConfiguration.projectileNumber;
    }

    protected override void SpawnProjectile(GameObject projectile, ProjectileConfiguration projectileConfiguration,
                                            Vector2 direction)
    {
        float multiplier = 160f / projectileNumber;

        for (int i = 0; i < projectileNumber; i++)
        {
            Vector2 dir = Quaternion.AngleAxis((-45 + (multiplier * i)), Vector3.forward) * direction;
            base.SpawnProjectile(projectile, projectileConfiguration, dir);
        }
    }
}
