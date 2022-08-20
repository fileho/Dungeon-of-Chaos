using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : IAttack {

    protected GameObject projectile;

    protected float wandReach = 1f;
    protected float wandWave = 10f;


    protected override void SetIndicatorTransform() {
        indicatorTransform = transform.Find(INDICATOR_SPAWN_POSITION);
    }

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        projectile = (attackConfiguration as RangedAttackConfiguration).projectile;
        wandReach = (attackConfiguration as RangedAttackConfiguration).wandReach;
        wandWave = (attackConfiguration as RangedAttackConfiguration).wandWave;
    }


    public override void Attack() {
        if (isAttacking)
            return;

        isAttacking = true;
        cooldownLeft = cooldown;
        PrepareWeapon();
        ActivateIndicator();
        StartCoroutine(StartAttackAnimation(wandWave, wandReach));
    }

    private IEnumerator StartAttackAnimation(float wandWave, float wandReach) {
        Vector3 startPos = weapon.transform.localPosition;
        Vector3 endPos = startPos + weapon.GetForwardDirection() * wandReach;
        var rot = weapon.transform.localRotation;

        float time = 0;
        //float duration = 0.6f;
        while (time < AttackAnimationDuration) {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / AttackAnimationDuration);
            weapon.transform.localPosition = Vector3.Lerp(startPos, endPos, t * (1 - t) * 4);

            float setup = 0.2f;
            if (t < setup)
                weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z - wandWave), t / setup);
            else if (1 - t < setup)
                weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z + wandWave), (1 - t) / setup);
            else {
                weapon.transform.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(0, 0, rot.eulerAngles.z - wandWave),
                    Quaternion.Euler(0, 0, rot.eulerAngles.z + wandWave),
                    (t - setup) / (1 - 2 * setup));
            }
            yield return null;
        }
        SpawnProjectile(projectile);

        transform.localRotation = rot;
        // Reset
        ResetWeapon();
        isAttacking = false;
    }


    protected void SpawnProjectile(GameObject projectile) {
        GameObject _projectile = Instantiate(projectile, indicatorTransform.position, indicatorTransform.rotation, transform);
        _projectile.GetComponent<IProjectile>().SetAttack(this);
    }

}
