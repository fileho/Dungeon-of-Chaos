using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SmashAttack : MeleeAttack {
    // Weapon lift before stomping
    protected float lift;

    // Distance below the default position that the weapon travels while stomping
    protected float fall;

    // How big the weapon grows
    protected float scaleMultiplier;

    //Damage radius
    protected float damageRadius;

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        SmashAttackConfiguration _attackConfiguration = attackConfiguration as SmashAttackConfiguration;
        fall = _attackConfiguration.fall;
        lift = _attackConfiguration.lift;
        damageRadius = _attackConfiguration.damageRadius;
        scaleMultiplier = _attackConfiguration.scaleMultiplier;
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
        Vector3 endPosdown = startPos + (-owner.transform.right * range) + (Vector3.down * fall);
        endPosdown += Weapon.transform.position - Weapon.Asset.transform.position;

        Vector3 startScale = Weapon.Asset.localScale;
        Vector3 endScale = Weapon.Asset.localScale * scaleMultiplier;

        ActivateIndicator();
        Vector3 indicatorPos = endPosdown - Weapon.transform.position + Weapon.Asset.transform.position;
        indicator.transform.localPosition = indicatorPos;
        indicator.transform.localScale *= damageRadius;
        indicatorPos = indicator.transform.position;
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
        var startRotation = Weapon.Asset.localRotation;
        float startRotationZ = Weapon.Asset.localRotation.eulerAngles.z < 180 ? Weapon.Asset.localRotation.eulerAngles.z : Weapon.Asset.localRotation.eulerAngles.z - 360f;

        time = 0;
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutElastic(time);
            Weapon.transform.localPosition = Vector3.Slerp(endPosUp - startPos, endPosdown - startPos, currentPos) + startPos;
            Weapon.Asset.localRotation = Quaternion.Lerp(startRotation, Quaternion.Euler(0, 0, Weapon.GetArmOffsetAngle()), currentPos);
            yield return null;
        }

        CheckHits(indicatorPos, damageRadius);

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
