using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class SwingAttack : MeleeAttack {

    public override void Attack() {
        base.Attack();
        StartCoroutine(StartAttackAnimation(swing, reach));
    }

    private IEnumerator StartAttackAnimation(float swing, float reach) {
        yield return new WaitForSeconds(IndicatorDuration);
        Vector3 startPos = Weapon.transform.localPosition;
        Vector3 endPos = startPos + Weapon.GetForwardDirection() * reach;
        var rot = Weapon.transform.localRotation;

        SoundManager.instance.PlaySound(swingSFX);
        PrepareWeapon();
        float time = 0;
        while (time < AttackAnimationDuration) {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / AttackAnimationDuration);
            Weapon.transform.localPosition = Vector3.Lerp(startPos, endPos, t * (1 - t) * 4);

            float setup = 0.2f;
            if (t < setup)
                Weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z - swing), t / setup);
            else if (1 - t < setup)
                Weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z + swing), (1 - t) / setup);
            else {
                Weapon.transform.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(0, 0, rot.eulerAngles.z - swing),
                    Quaternion.Euler(0, 0, rot.eulerAngles.z + swing),
                    (t - setup) / (1 - 2 * setup));
            }
            yield return null;
        }
        transform.localRotation = rot;

        // Reset
        ResetWeapon();
        isAttacking = false;
    }


}
