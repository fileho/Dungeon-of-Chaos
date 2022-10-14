using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif



public abstract class MeleeAttack : IAttack {

    // Angle sweeped during the attack animation
    protected float swing;
    // Reach is how far the weapon travels during the attack animation
    protected float reach;

    protected const string INDICATOR_SPAWN_POSITION = "MeleeIndicatorSpawnPosition";

    protected override void SetIndicatorTransform() {
        indicatorTransform = owner.transform.Find(INDICATOR_SPAWN_POSITION);
    }


    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        MeleeAttackConfiguration _attackConfiguration = attackConfiguration as MeleeAttackConfiguration;
        swing = _attackConfiguration.swing;
        reach = _attackConfiguration.reach;
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
