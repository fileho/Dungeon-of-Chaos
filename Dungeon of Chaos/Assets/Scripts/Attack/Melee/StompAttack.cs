using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class StompAttack : MeleeAttack {


    // Weapon lift before stomping
    protected float lift;
    // How big the weapon grows
    protected float scaleMultiplier;

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        StompAttackConfiguration _attackConfiguration = attackConfiguration as StompAttackConfiguration;
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

        Vector3 startPos = Weapon.Asset.position;
        Vector3 endPos = startPos + Vector3.up * lift;

        Vector3 startScale = Weapon.Asset.localScale;
        Vector3 endScale = Weapon.Asset.localScale * scaleMultiplier;

        float time = 0;
        float attackAnimationDurationOneWay = AttackAnimationDuration / 2f;

        // Up
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutExpo(time);
            Weapon.Asset.position = Vector3.Lerp(startPos, endPos, currentPos);
            Weapon.Asset.localScale = Vector3.Lerp(startScale, endScale, currentPos);
            yield return null;
        }

        // Down
        time = 0;
        while (time <= 1) {
            time += (Time.deltaTime / attackAnimationDurationOneWay);
            float currentPos = Tweens.EaseOutElastic(time);
            Weapon.Asset.position = Vector3.Lerp(endPos, startPos, currentPos);
            Weapon.Asset.localScale = Vector3.Lerp(endScale, startScale, currentPos);

            yield return null;
        }

        SoundManager.instance.PlaySound(swingSFX);
        yield return new WaitForSeconds(0.2f);

        // Reset
        Weapon.Asset.position = startPos;
        Weapon.Asset.localScale = startScale;
        ResetWeapon();

        yield return new WaitForSeconds(1);
        isAttacking = false;
    }


}
