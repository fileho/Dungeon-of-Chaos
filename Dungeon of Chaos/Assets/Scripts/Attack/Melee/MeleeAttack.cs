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

    protected override void SetIndicatorTransform() {
        indicatorTransform = owner.transform.Find(INDICATOR_SPAWN_POSITION);
    }


    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        swing = (attackConfiguration as MeleeAttackConfiguration).swing;
        reach = (attackConfiguration as MeleeAttackConfiguration).reach;
    }

    
    public override void Attack() {
        if (isAttacking)
            return;

        isAttacking = true;
        cooldownLeft = cooldown;

        PrepareWeapon();
        ActivateIndicator();
    }

}
