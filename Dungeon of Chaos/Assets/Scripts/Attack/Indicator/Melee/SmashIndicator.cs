using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashIndicator : MeleeIndicator {

    protected override IEnumerator ShowIndicator() {
        float time = 0f;
        while (time < 1) {
            time += (Time.deltaTime / Duration);
            float currentRotation = Tweens.EaseOutQuadratic(time);
            float currentColor = Tweens.EaseOutQuadratic(time);

            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentColor);
            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 180), currentRotation);
            yield return null;
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        CleanUp();
    }
}