using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class SwingAttack : MeleeAttack {

    // Angle sweeped during the attack animation
    protected float swing;

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        SwingAttackConfiguration _attackConfiguration = attackConfiguration as SwingAttackConfiguration;
        swing = _attackConfiguration.swing;
    }

    protected override void PrepareWeapon() {
        base.PrepareWeapon();
        Weapon.EnableDisableCollider(true);
    }


    protected override void ResetWeapon() {
        base.ResetWeapon();
        Weapon.EnableDisableCollider(false);
    }

    // Ideal attack duration = 1
    protected override IEnumerator StartAttackAnimation() {

        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;

        float angleMultiplier = Weapon.transform.lossyScale.x > 0 ? 1 : -1;
        float swingAdjusted = swing * angleMultiplier;

        ActivateIndicator();
        yield return new WaitForSeconds(IndicatorDuration);

        PrepareWeapon();

        // Cache weapon rotation to restore after the animation
        var initialAssetRotation = Weapon.Asset.localRotation;
        Weapon.Asset.localRotation = Quaternion.Euler(0, 0, Weapon.GetArmOffsetAngle());

        Vector3 upperEdge = Quaternion.AngleAxis((-swingAdjusted / 2f), Vector3.forward) * targetDirection;
        upperEdge = Weapon.transform.lossyScale.x > 0 ? upperEdge : -Vector3.Reflect(upperEdge, Vector2.up);
        Vector3 lowerEdge = Quaternion.AngleAxis((swingAdjusted / 2f), Vector3.forward) * targetDirection;
        lowerEdge = Weapon.transform.lossyScale.x > 0 ? lowerEdge : -Vector3.Reflect(lowerEdge, Vector2.up);

        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPosUp = startPos + (upperEdge * (range));
        Vector3 endPosdown = startPos + (lowerEdge * (range));


        float time = 0;
        float attackAnimationDurationOneWay = AttackAnimationDuration / 3f;

        // Forward
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            Weapon.transform.localPosition = Vector3.Lerp(startPos, endPosUp, time);
            yield return null;
        }

        // Sweep
        var startRotation = Weapon.Asset.localRotation;
        float startRotationZ = Weapon.Asset.localRotation.eulerAngles.z < 180 ? Weapon.Asset.localRotation.eulerAngles.z : Weapon.Asset.localRotation.eulerAngles.z - 360f;

        time = 0;
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutExponential(time);
            Weapon.transform.localPosition = Vector3.Slerp(endPosUp - startPos, endPosdown - startPos, currentPos) + startPos;
            yield return null;
        }

        // Backward
        time = 0;
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            Weapon.transform.localPosition = Vector3.Lerp(endPosdown, startPos, time);
            yield return null;
        }

        SoundManager.instance.PlaySound(swingSFX);

        // Reset
        Weapon.Asset.localRotation = initialAssetRotation;
        Weapon.transform.localPosition = startPos;

        ResetWeapon();
        isAttacking = false;
    }


}
