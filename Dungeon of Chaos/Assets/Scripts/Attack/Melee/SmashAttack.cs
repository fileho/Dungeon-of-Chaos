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

    public override void Attack() {
        base.Attack();
        StartCoroutine(StartAttackAnimation());
    }


    private void CheckHits(Vector3 pos, float radius)
    {
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

        CheckHits(endPosdown, damageRadius);

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
