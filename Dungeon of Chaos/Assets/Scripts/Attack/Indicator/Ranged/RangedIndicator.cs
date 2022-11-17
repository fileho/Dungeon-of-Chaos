using System.Collections;
using UnityEngine;

public class RangedIndicator : IIndicator {

    private Vector2 initialScale;
    private Vector2 finalScale;

    protected override void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration) {
        base.ApplyConfigurations(indicatorConfiguration);
        RangedIndicatorConfiguration _indicatorConfiguration = indicatorConfiguration as RangedIndicatorConfiguration;
        initialScale = _indicatorConfiguration.initialScale;
        finalScale = _indicatorConfiguration.finalScale;
    }

    protected override IEnumerator ShowIndicator() {
        float time = 0f;
        while (time < 1) {
            time += (Time.deltaTime / Duration);
            float currentColor = Tweens.EaseOutQuadratic(time);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, currentColor);
            transform.localScale = Vector3.Lerp(initialScale, finalScale, currentColor);
            yield return null;
        }
        transform.localScale = finalScale;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        CleanUp();
    }
}
