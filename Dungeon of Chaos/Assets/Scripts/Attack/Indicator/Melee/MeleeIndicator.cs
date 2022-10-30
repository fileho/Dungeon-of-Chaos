using System.Collections;
using UnityEngine;

public class MeleeIndicator : IIndicator {

    protected override IEnumerator ShowIndicator() {
        float time = 0f;
        while (time < 1) {
            time += (Time.deltaTime / Duration);
            float currentColor = Tweens.EaseInCubic(time);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentColor);
            yield return null;
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        yield return new WaitForSeconds(0.1f);
        CleanUp();
    }

    public override void Use() {
        StartCoroutine(ShowIndicator());
    }
}
