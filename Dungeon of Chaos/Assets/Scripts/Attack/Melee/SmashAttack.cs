using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : MeleeAttack
{
    // Weapon lift before stomping
    protected float lift;

    // Distance below the default position that the weapon travels while stomping
    protected float fall;

    // How big the weapon grows
    protected float scaleMultiplier;

    //Damage radius
    protected float damageRadius;

    protected GameObject impact;

    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        SmashAttackConfiguration _attackConfiguration = attackConfiguration as SmashAttackConfiguration;
        fall = _attackConfiguration.fall;
        lift = _attackConfiguration.lift;
        impact = _attackConfiguration.impact;
        damageRadius = _attackConfiguration.damageRadius;
        scaleMultiplier = _attackConfiguration.scaleMultiplier;
    }

    protected void EnableImpact(Vector3 pos)
    {
        GameObject impactPs = Instantiate(impact, pos, Quaternion.identity);
        Destroy(impactPs, 5f);
    }


    private void CheckHits(Vector3 pos, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, radius);
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Unit>())
                {
                    Weapon.InflictDamage(colliders[i].GetComponent<Unit>());
                }
            }
        }
    }

    // Ideal attack duration = 1
    protected override IEnumerator StartAttackAnimation()
    {

        // Cache weapon rotation to restore it after the animation
        var initialWeaponRotation = Weapon.transform.rotation;
        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;
        Vector3 startPos = Weapon.transform.localPosition;


        float angleMultiplier = Weapon.transform.lossyScale.x > 0 ? 1 : -1;
        float swingAdjusted = 90 * angleMultiplier;

        Vector3 upperEdge = Quaternion.AngleAxis(-swingAdjusted, Vector3.forward) * targetDirection;
        upperEdge = Weapon.transform.lossyScale.x > 0 ? upperEdge : -Vector3.Reflect(upperEdge, Vector2.up);
        Vector3 lowerEdge = targetDirection;
        lowerEdge = Weapon.transform.lossyScale.x > 0 ? lowerEdge : -Vector3.Reflect(lowerEdge, Vector2.up);

        Vector3 endPosUp = startPos + (upperEdge * range);
        Vector3 endPosdown = startPos + (lowerEdge * range);
        Vector3 endPosUpAdjusted = startPos + (upperEdge * (range - 2 * Weapon.WeaponAssetWidth));  //to compensate for the weapon asset width
        Vector3 endPosdownAdjusted = startPos + (lowerEdge * (range - 2 * Weapon.WeaponAssetWidth)); //to compensate for the weapon asset width



        Vector3 startScale = Weapon.Asset.localScale;
        Vector3 endScale = Weapon.Asset.localScale * scaleMultiplier;

        IIndicator indicator = CreateIndicator();
        if (indicator)
        {
            indicator.transform.position = owner.transform.TransformPoint(endPosdown);
            indicator.transform.localScale *= damageRadius;
            indicator.Use();
            yield return new WaitForSeconds(indicator.Duration);
        }

        PrepareWeapon();

        float time = 0;
        float attackAnimationDurationOneWay = AttackAnimationDuration / 2f;

        // Up
        while (time <= 1)
        {
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
        while (time <= 1)
        {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutElastic(time);
            Weapon.transform.localPosition = Vector3.Slerp(endPosUp - startPos, endPosdownAdjusted - startPos, currentPos) + startPos;
            yield return null;
        }

        EnableImpact(owner.transform.TransformPoint(endPosdown));
        CheckHits(owner.transform.TransformPoint(endPosdown), damageRadius);
        SoundManager.instance.PlaySound(swingSFX);

        // Reset
        Weapon.Asset.localScale = startScale;
        Weapon.transform.localPosition = startPos;
        Weapon.transform.rotation = initialWeaponRotation;

        ResetWeapon();
        isAttacking = false;
    }

}
