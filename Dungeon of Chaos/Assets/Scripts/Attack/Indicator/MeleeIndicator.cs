using System.Collections;
using UnityEngine;

public class MeleeIndicator : IIndicator {
    public override void Use() {
        StartCoroutine(ShowIndicator());
    }
}
