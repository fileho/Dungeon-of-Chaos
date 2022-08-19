using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif



public abstract class MeleeAttack : IAttack {

    // Angle sweeped during the weapon animation
    protected float swing;
    // Reach is how far the weapon travels during the attack animation
    protected float reach;
    // Duration of the swing
    protected float duration;


    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        swing = (attackConfiguration as MeleeAttackConfiguration).swing;
        reach = (attackConfiguration as MeleeAttackConfiguration).reach;
        duration = (attackConfiguration as MeleeAttackConfiguration).duration;
    }

    
    public override void Attack() {
        if (isAttacking)
            return;

        isAttacking = true;
        cooldownLeft = cooldown;

        ActivateIndicator();
        PrepareWeapon();
    }

}
