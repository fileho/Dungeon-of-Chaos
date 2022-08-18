using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif


public abstract class MeeleAttack : IAttack {
    public override void Attack() {
        
    }


    public override bool CanAttack() {
        return cooldownLeft <= 0;
    }


    protected override void Start() {
        base.Start();
    }

    protected virtual void Update() {
        if (!isAttacking && cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }
}
