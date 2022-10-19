using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAttack : MeleeAttack {


    // Weapon lift before stomping
    protected float lift;

    // Distance below the default position that the weapon travels while stomping
    protected float fall;

    // How big the weapon grows
    protected float scaleMultiplier;

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        StompAttackConfiguration _attackConfiguration = attackConfiguration as StompAttackConfiguration;
        fall = _attackConfiguration.fall;
        lift = _attackConfiguration.lift;
        scaleMultiplier = _attackConfiguration.scaleMultiplier;
    }


    public override void Attack() {
        base.Attack();
        StartCoroutine(StartAttackAnimation());
    }


    // Ideal attack duration = 1
    private IEnumerator StartAttackAnimation() {

        Weapon.ResetWeapon();
        yield return new WaitForSeconds(IndicatorDuration);

        PrepareWeapon();

        Vector3 startPos = Weapon.transform.position;
        Vector3 endPosUp = startPos + Vector3.up * lift;

        float directionMultiplier = owner.transform.localScale.x == 1 ? -1 : 1;
        Vector3 endPosdown = startPos + (owner.transform.right * directionMultiplier * range) + (Vector3.down * fall);

        Vector3 startScale = Weapon.Asset.localScale;
        Vector3 endScale = Weapon.Asset.localScale * scaleMultiplier;

        var rot = Weapon.Asset.localRotation;

        float time = 0;
        float attackAnimationDurationOneWay = AttackAnimationDuration / 2f;

        // Up
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutExpo(time);
            Weapon.transform.position = Vector3.Lerp(startPos, endPosUp, currentPos);
            Weapon.Asset.localScale = Vector3.Lerp(startScale, endScale, currentPos);
            yield return null;
        }

        // Down
        time = 0;
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutElastic(time);
            Weapon.transform.position = Vector3.Slerp(endPosUp - startPos, endPosdown - startPos, currentPos) + startPos;
            Weapon.Asset.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, 90), currentPos);
            yield return null;
        }

        SoundManager.instance.PlaySound(swingSFX);
        yield return new WaitForSeconds(0.2f);

        // Reset
        Weapon.transform.position = startPos;
        Weapon.Asset.localScale = startScale;
        Weapon.Asset.localRotation = rot;
        ResetWeapon();

        yield return new WaitForSeconds(1);
        isAttacking = false;
    }


}
