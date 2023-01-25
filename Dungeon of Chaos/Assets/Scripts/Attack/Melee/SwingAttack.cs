using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class SwingAttack : MeleeAttack
{

    // Angle sweeped during the attack animation
    protected float swing;

    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        SwingAttackConfiguration _attackConfiguration = attackConfiguration as SwingAttackConfiguration;
        swing = _attackConfiguration.swing;
    }

    protected override void PrepareWeapon()
    {
        base.PrepareWeapon();
        Weapon.EnableDisableCollider(true);
    }

    protected override void ResetWeapon()
    {
        base.ResetWeapon();
        Weapon.EnableDisableCollider(false);
    }

    // Ideal attack duration = 1
    protected override IEnumerator StartAttackAnimation()
    {

        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;

        float angleMultiplier = Weapon.transform.lossyScale.x > 0 ? 1 : -1;
        float swingAdjusted = swing * angleMultiplier;

        Vector3 upperEdge = Quaternion.AngleAxis((-swingAdjusted / 2f), Vector3.forward) * targetDirection;
        upperEdge = Weapon.transform.lossyScale.x > 0 ? upperEdge : -Vector3.Reflect(upperEdge, Vector2.up);
        Vector3 lowerEdge = Quaternion.AngleAxis((swingAdjusted / 2f), Vector3.forward) * targetDirection;
        lowerEdge = Weapon.transform.lossyScale.x > 0 ? lowerEdge : -Vector3.Reflect(lowerEdge, Vector2.up);

        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPosUp = startPos + (upperEdge * range);
        Vector3 endPosdown = startPos + (lowerEdge * range);
        Vector3 endPosUpAdjusted =
            startPos + (upperEdge * (range - Weapon.WeaponAssetWidth)); // to compensate for the weapon asset width
        Vector3 endPosdownAdjusted =
            startPos + (lowerEdge * (range - Weapon.WeaponAssetWidth)); // to compensate for the weapon asset width

        IIndicator indicator = CreateIndicator();
        if (indicator)
        {
            indicator.transform.localPosition = owner.transform.InverseTransformPoint(Weapon.Asset.transform.position);
            ;
            // indicator.transform.up = Weapon.GetForwardDirectionRotated();
            indicator.transform.up = targetDirection;
            indicator.Use();
            yield return new WaitForSeconds(indicator.Duration);
        }

        PrepareWeapon();

        // Cache weapon rotation to restore after the animation
        var initialAssetRotation = Weapon.Asset.localRotation;
        Weapon.Asset.localRotation = Quaternion.Euler(0, 0, Weapon.GetArmAlignAngle());

        float time = 0;
        float attackAnimationDurationOneWay = AttackAnimationDuration / 3f;

        SoundManager.instance.PlaySound(swingSFX);
        // Forward
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            Weapon.transform.localPosition = Vector3.Lerp(startPos, endPosUpAdjusted, time);
            yield return null;
        }

        // Sweep
        var startRotation = Weapon.Asset.localRotation;
        float startRotationZ = Weapon.Asset.localRotation.eulerAngles.z < 180
                                   ? Weapon.Asset.localRotation.eulerAngles.z
                                   : Weapon.Asset.localRotation.eulerAngles.z - 360f;

        time = 0;
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutExponential(time);
            Weapon.transform.localPosition =
                Vector3.Slerp(endPosUpAdjusted - startPos, endPosdownAdjusted - startPos, currentPos) + startPos;
            yield return null;
        }

        // Backward
        time = 0;
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            Weapon.transform.localPosition = Vector3.Lerp(endPosdownAdjusted, startPos, time);
            yield return null;
        }

        // Reset
        Weapon.Asset.localRotation = initialAssetRotation;
        Weapon.transform.localPosition = startPos;

        ResetWeapon();
        isAttacking = false;
    }
}
