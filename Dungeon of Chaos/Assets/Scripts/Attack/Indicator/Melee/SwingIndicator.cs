using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingIndicator : MeleeIndicator {
    private float sweep;

    protected override void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration) {
        base.ApplyConfigurations(indicatorConfiguration);
        SwingIndicatorConfiguration _indicatorConfiguration = indicatorConfiguration as SwingIndicatorConfiguration;
        sweep = _indicatorConfiguration.sweep;
        sprite.material = new Material(sprite.material);
    }

    protected override IEnumerator ShowIndicator() {
        float time = 0f;
        while (time < 1) {
            time += (Time.deltaTime / Duration);
            float currentState = Tweens.EaseOutExponential(time);
            float currentValue = Mathf.Lerp(0, sweep, currentState);
            sprite.material.SetFloat("_Arc1", 180f - currentValue / 2f);
            sprite.material.SetFloat("_Arc2", 180f - currentValue / 2f);

            yield return null;
        }
        sprite.material.SetFloat("_Arc1", 180f - sweep / 2f);
        sprite.material.SetFloat("_Arc2", 180f - sweep / 2f);
        yield return new WaitForSeconds(0.1f);
        CleanUp();
    }

}