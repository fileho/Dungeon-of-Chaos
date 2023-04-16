using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : IAttack
{
    protected GameObject projectile;
    protected ProjectileConfiguration projectileConfiguration;
    protected float wandReach = 1f;

    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        RangedAttackConfiguration _attackConfiguration = attackConfiguration as RangedAttackConfiguration;
        projectile = _attackConfiguration.projectile;
        projectileConfiguration = _attackConfiguration.projectileConfiguration;
        wandReach = _attackConfiguration.wandReach;
    }

    // Ideal attack duration = 1
    protected override IEnumerator StartAttackAnimation()
    {
        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;

        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPos = startPos + (targetDirection * wandReach);
        endPos = Weapon.transform.lossyScale.x > 0 ? endPos : -Vector3.Reflect(endPos, Vector2.up);
        Vector3 midPos = (endPos + startPos) / 2f;

        IIndicator indicator = CreateIndicator(transform);
        if (indicator)
        {
            indicator.transform.localPosition = Weapon.GetTrailLocalPosition();
            indicator.Use();
            yield return new WaitForSeconds(indicator.Duration);
        }

        PrepareWeapon();

        // Cache weapon rotation to restore after the animation
        var initialAssetRotation = Weapon.Asset.localRotation;
        Weapon.Asset.localRotation = Quaternion.Euler(0, 0, Weapon.GetUprightAngle());

        float attackAnimationDurationOneWay = AttackAnimationDuration / 2f;

        // Forward
        float time = 0;
        midPos -= new Vector3(0, 0.1f);
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseInCubic(time);
            Weapon.transform.localPosition = Vector3.Slerp(startPos - midPos, endPos - midPos, currentPos) + midPos;
            yield return null;
        }

        targetDirection = (GetTargetPosition() - (Vector2)Weapon.GetTrailPosition()).normalized;
        SpawnProjectile(projectile, projectileConfiguration, targetDirection);

        // Backward
        time = 0;
        midPos += new Vector3(0, 0.2f);
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutCubic(time);
            Weapon.transform.localPosition = Vector3.Slerp(endPos - midPos, startPos - midPos, currentPos) + midPos;
            yield return null;
        }

        SoundManager.instance.PlaySound(attackSFX);

        // Reset
        Weapon.Asset.localRotation = initialAssetRotation;
        Weapon.transform.localPosition = startPos;

        ResetWeapon();
        isAttacking = false;
    }

    protected virtual void SpawnProjectile(GameObject projectile, ProjectileConfiguration projectileConfiguration,
                                           Vector2 direction)
    {
        GameObject _projectile = Instantiate(projectile, transform);
        _projectile.transform.localPosition = Weapon.GetTrailLocalPosition();
        _projectile.transform.parent = null;
        IProjectile iProjectile = _projectile.GetComponent<IProjectile>();
        iProjectile.Init(this, projectileConfiguration);
        iProjectile.Launch(direction);
    }
}
