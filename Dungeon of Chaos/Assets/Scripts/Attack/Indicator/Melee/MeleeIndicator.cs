using System.Collections;
using UnityEngine;

public class MeleeIndicator : IIndicator {

    protected override IEnumerator ShowIndicator() {
        float time = 0f;
        while (time < Duration) {
            time += Time.deltaTime;
            float t = time / Duration;
            t = (t + 0.3f) * 0.3f;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, t);
            yield return null;
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        CleanUp();
    }

    public override void Use() {
        StartCoroutine(ShowIndicator());
    }
}
