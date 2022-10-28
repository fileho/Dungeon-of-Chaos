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

    public override void Use() {
        StartCoroutine(ShowIndicator());
    }

    protected override IEnumerator ShowIndicator() {
        float time = 0f;
        while (time < Duration) {
            time += Time.deltaTime;
            float t = time / Duration;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, t);
            transform.localScale = Vector3.Lerp(initialScale, finalScale, t);
            yield return null;
        }
        transform.localScale = finalScale;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        CleanUp();
    }
}
