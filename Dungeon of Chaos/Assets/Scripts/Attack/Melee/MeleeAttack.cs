using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif



public abstract class MeleeAttack : IAttack {

    protected const string INDICATOR_SPAWN_POSITION = "MeleeIndicatorSpawnPosition";

    protected override void SetIndicatorTransform() {
        indicatorTransform = owner.transform.Find(INDICATOR_SPAWN_POSITION);
    }

    protected override void PrepareWeapon()
    {
        base.PrepareWeapon();
        Weapon.EnableDisableTrail(true);
        Weapon.EnableDisableCollider(true);
    }

    public override void Attack() {
        if (isAttacking)
            return;

        isAttacking = true;
        cooldownLeft = cooldown;

        ActivateIndicator();
    }

    public override string ToString() {
        return base.ToString() + "_Melee";
    }
}
