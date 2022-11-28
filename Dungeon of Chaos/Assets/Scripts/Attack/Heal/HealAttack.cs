using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HealAttack : IAttack
{

    protected float healAmount = 2;
    protected float healRadius = 10;

    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        HealAttackConfiguration _attackConfiguration = attackConfiguration as HealAttackConfiguration;
        healAmount = _attackConfiguration.healAmount;
        healRadius = _attackConfiguration.healRadius;
    }


    protected override IEnumerator StartAttackAnimation()
    {
        Vector3 weaponPos = Weapon.transform.position;
        Vector3 targetDirection = (GetTargetPosition() - (Vector2)weaponPos).normalized;

        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPos = startPos + (targetDirection);
        endPos = Weapon.transform.lossyScale.x > 0 ? endPos : -Vector3.Reflect(endPos, Vector2.up);
        Vector3 midPos = (endPos + startPos) / 2f;

        IIndicator indicator = CreateIndicator(transform);
        if (indicator)
        {
            indicator.transform.localPosition = Weapon.GetWeaponTipOffset();
            indicator.Use();
            yield return new WaitForSeconds(indicator.Duration);
        }


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

        SoundManager.instance.PlaySound(swingSFX);

        // Reset
        Weapon.Asset.localRotation = initialAssetRotation;
        Weapon.transform.localPosition = startPos;

        Heal();
        isAttacking = false;
    }


    private void Heal()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(owner.transform.position, healRadius, 1 << LayerMask.NameToLayer("Enemy"));
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].GetComponent<Unit>())
                {
                    // Increase health
                    colliders[i].GetComponent<Unit>().stats.RegenerateHealth(healAmount);
                }
            }
        }
    }

    public override string ToString()
    {
        return base.ToString() + "_Ranged";
    }
}
