using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class StompAttack : MeleeAttack {

    // Weapon lift before stomping
    protected float lift;

    // Distance below the default position that the weapon travels while stomping
    protected float fall;

    // How big the weapon grows
    protected float scaleMultiplier;

    //Damage minor [It is the default range of the attack]
    protected float damageMinor;
    //Damage major [should be more than the default damage]
    protected float damageMajor;

    //Damage radius minor [It is the default damage of the attack]
    protected float damageRadiusMinor;
    //Damage radius major [Should be less than the default range of the attack ]
    protected float damageRadiusMajor;


    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        StompAttackConfiguration _attackConfiguration = attackConfiguration as StompAttackConfiguration;
        fall = _attackConfiguration.fall;
        lift = _attackConfiguration.lift;
        scaleMultiplier = _attackConfiguration.scaleMultiplier;
        damageRadiusMajor = _attackConfiguration.damageRadiusMajor;
        damageMajor = _attackConfiguration.damageMajor;
        damageRadiusMinor = range;
        damageMinor = damage;
    }


    private void CheckHits(Vector3 pos, float radius) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius);
        if (colliders.Length > 0) {
            for (int i = 0; i < colliders.Length; i++) {
                if (colliders[i].GetComponent<Unit>()) {
                    Weapon.InflictDamage(colliders[i].GetComponent<Unit>());
                }
            }
        }
    }

    // Ideal attack duration = 1
    protected override IEnumerator StartAttackAnimation() {

        // Cache weapon rotation to restore it after the animation
        var initialWeaponRotation = Weapon.transform.rotation;
        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPosUp = startPos + Vector3.up * lift;
        Vector3 endPosdown = startPos + Vector3.down * fall;

        Vector3 startScale = Weapon.Asset.localScale;
        Vector3 endScale = Weapon.Asset.localScale * scaleMultiplier;

        ActivateIndicator();
        Vector3 indicatorPos = indicator.transform.position;
        yield return new WaitForSeconds(IndicatorDuration);

        // Reset weapon rotation to default for the animation
        Weapon.ResetWeapon();
        PrepareWeapon();


        // Cache weapon rotation to restore after the animation
        var initialAssetRotation = Weapon.Asset.localRotation;
        Weapon.Asset.localRotation = Quaternion.Euler(0, 0, Weapon.GetUprightAngle());



        float time = 0;
        float attackAnimationDurationOneWay = AttackAnimationDuration / 2f;

        // Up
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutExponential(time);
            Weapon.transform.localPosition = Vector3.Lerp(startPos, endPosUp, currentPos);
            Weapon.Asset.localScale = Vector3.Lerp(startScale, endScale, currentPos);
            yield return null;
        }

        // Down
        time = 0;
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutElastic(time);
            Weapon.transform.localPosition = Vector3.Lerp(endPosUp, endPosdown, currentPos);
            yield return null;
        }

        Weapon.SetDamage(damageMajor);
        CheckHits(owner.transform.position, damageRadiusMajor);
        Weapon.SetDamage(damageMinor);
        CheckHits(owner.transform.position, damageRadiusMinor);

        SoundManager.instance.PlaySound(swingSFX);

        // Reset
        Weapon.Asset.localScale = startScale;
        Weapon.Asset.localRotation = initialAssetRotation;
        Weapon.transform.localPosition = startPos;
        Weapon.transform.rotation = initialWeaponRotation;

        ResetWeapon();
        isAttacking = false;
    }

}
