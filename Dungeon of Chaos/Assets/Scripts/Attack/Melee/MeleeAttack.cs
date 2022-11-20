using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
#endif



public abstract class MeleeAttack : IAttack {

    public override string ToString() {
        return base.ToString() + "_Melee";
    }
}
