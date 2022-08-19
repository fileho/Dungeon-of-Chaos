using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : IAttack {

    protected GameObject projectile;

    private const float WAND_REACH = 1f;
    private const float WAND_WAVE = 10f;

    protected override void Start() {
        base.Start();
    }

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        projectile = (attackConfiguration as RangedAttackConfiguration).projectile;
    }


    public override void Attack() {
        if (isAttacking)
            return;

        isAttacking = true;
        cooldownLeft = cooldown;
        ActivateIndicator();
        PrepareWeapon();
        StartCoroutine(StartAttackAnimation());
    }

    private IEnumerator StartAttackAnimation() {
        yield return new WaitForSeconds(delayAfterIndicator);
        Vector3 startPos = weapon.transform.localPosition;
        Vector3 endPos = startPos + weapon.GetForwardDirection() * WAND_REACH;
        var rot = weapon.transform.localRotation;

        float time = 0;
        float duration = 0.6f;
        while (time < duration) {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            weapon.transform.localPosition = Vector3.Lerp(startPos, endPos, t * (1 - t) * 4);

            float setup = 0.2f;
            if (t < setup)
                weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z - WAND_WAVE), t / setup);
            else if (1 - t < setup)
                weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z + WAND_WAVE), (1 - t) / setup);
            else {
                weapon.transform.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(0, 0, rot.eulerAngles.z - WAND_WAVE),
                    Quaternion.Euler(0, 0, rot.eulerAngles.z + WAND_WAVE),
                    (t - setup) / (1 - 2 * setup));
            }
            yield return null;
        }


        transform.localRotation = rot;
        SpawnProjectile(projectile);

        // Reset
        ResetWeapon();
        isAttacking = false;
    }


    protected virtual void SpawnProjectile(GameObject projectile) {
        GameObject _projectile = Instantiate(projectile, transform.position, transform.rotation, weapon.transform);
        _projectile.GetComponent<IProjectile>().SetAttack(this);
    }

}
