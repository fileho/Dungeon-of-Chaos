using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : IAttack {

    protected GameObject projectile;

    protected float wandReach = 1f;
    protected const string INDICATOR_SPAWN_POSITION = "RangeIndicatorSpawnPosition";

    protected override void SetIndicatorTransform() {
        indicatorTransform = transform.Find(INDICATOR_SPAWN_POSITION);
        //print(indicatorTransform);
    }

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        RangedAttackConfiguration _attackConfiguration = attackConfiguration as RangedAttackConfiguration;
        projectile = _attackConfiguration.projectile;
        wandReach = _attackConfiguration.wandReach;
    }


    public override void Attack() {
        if (isAttacking)
            return;

        isAttacking = true;
        cooldownLeft = cooldown;
        ActivateIndicator();
        StartCoroutine(StartAttackAnimation());
    }

    // Ideal attack duration = 1
    private IEnumerator StartAttackAnimation() {

        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;

        yield return new WaitForSeconds(IndicatorDuration);

        PrepareWeapon();

        // Cache weapon rotation to restore after the animation
        var initialAssetRotation = Weapon.Asset.localRotation;
        Weapon.Asset.localRotation = Quaternion.Euler(0, 0, Weapon.GetUprightAngle());

        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPos = startPos + (targetDirection * wandReach);
        Vector3 midPos = (endPos + startPos) / 2f;

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
        GameObject _projectile = Instantiate(projectile, indicatorTransform.position, indicatorTransform.rotation);
        _projectile.GetComponent<IProjectile>().SetAttack(this);
    }


    public override string ToString() {
        return base.ToString() + "_Ranged";
    }
}
