using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : MeeleAttack {

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }


    // Update is called once per frame
    protected override void Update() {
        base.Update();
    }


    public override void Attack() {
        base.Attack();
        if (isAttacking)
            return;
        StartCoroutine(StartAttack(swing, damage, reach));
    }


    private void ActivateIndicator() {
        if (indicator == null) return;
        GameObject _indicator = Instantiate(indicator, transform.position, transform.rotation, transform);
        _indicator.transform.up = weapon.GetForwardDirectionRotated();
    }


    private IEnumerator StartAttack(float swing, float damage, float reach) {
        // Setup

        isAttacking = true;
        cooldownLeft = cooldown;
        ActivateIndicator();
        yield return new WaitForSeconds(delayAfterIndicator);

        weapon.EnableDisableTrail(true);
        weapon.EnableDisableCollider(true);
        weapon.SetDamage(damage);

        Vector3 startPos = weapon.transform.localPosition;
        Vector3 endPos = startPos + weapon.GetForwardDirection() * reach;
        var rot = weapon.transform.localRotation;

        float time = 0;
        float duration = .6f;
        while (time < duration) {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            weapon.transform.localPosition = Vector3.Lerp(startPos, endPos, t * (1 - t) * 4);

            float setup = 0.2f;
            if (t < setup)
                weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z - swing), t / setup);
            else if (1 - t < setup)
                weapon.transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z + swing), (1 - t) / setup);
            else {
                weapon.transform.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(0, 0, rot.eulerAngles.z - swing),
                    Quaternion.Euler(0, 0, rot.eulerAngles.z + swing),
                    (t - setup) / (1 - 2 * setup));
            }
            yield return null;
        }

        // Restore settings
        weapon.EnableDisableTrail(false);
        weapon.EnableDisableCollider(false);
        transform.localRotation = rot;
        isAttacking = false;
    }


}
