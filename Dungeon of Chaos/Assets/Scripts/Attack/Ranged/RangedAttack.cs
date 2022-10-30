using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : IAttack {

    protected GameObject projectile;

    protected float wandReach = 1f;


    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        RangedAttackConfiguration _attackConfiguration = attackConfiguration as RangedAttackConfiguration;
        projectile = _attackConfiguration.projectile;
        wandReach = _attackConfiguration.wandReach;
    }

    // Ideal attack duration = 1
    protected override IEnumerator StartAttackAnimation() {

        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;

        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPos = startPos + (targetDirection * wandReach);
        Vector3 midPos = (endPos + startPos) / 2f;

        IIndicator indicator = CreateIndicator(transform);
        if (indicator) {
            indicator.transform.up = Weapon.GetForwardDirectionRotated();
            indicator.transform.localPosition = Weapon.GetWeaponTipOffset();
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
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseInCubic(time);
            Weapon.transform.localPosition = Vector3.Slerp(startPos - midPos, endPos - midPos, currentPos) + midPos;
            yield return null;
        }

        SpawnProjectile(projectile);

        // Backward
        time = 0;
        midPos += new Vector3(0, 0.2f);
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutCubic(time);
            Weapon.transform.localPosition = Vector3.Slerp(endPos - midPos, startPos - midPos, currentPos) + midPos;
            yield return null;
        }

        SoundManager.instance.PlaySound(swingSFX);

        // Reset
        Weapon.Asset.localRotation = initialAssetRotation;
        Weapon.transform.localPosition = startPos;

        ResetWeapon();
        isAttacking = false;
    }


    protected void SpawnProjectile(GameObject projectile) {
        GameObject _projectile = Instantiate(projectile, transform.position + Weapon.GetWeaponTipOffset(), transform.rotation);
        _projectile.GetComponent<IProjectile>().SetAttack(this);
    }


    public override string ToString() {
        return base.ToString() + "_Ranged";
    }
}
