using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashIndicator : MeleeIndicator
{
    private float distance;

    protected override void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration)
    {
        base.ApplyConfigurations(indicatorConfiguration);
        DashIndicatorConfiguration _indicatorConfiguration = indicatorConfiguration as DashIndicatorConfiguration;
        distance = _indicatorConfiguration.distance;
    }

    protected override IEnumerator ShowIndicator()
    {
        float time = 0f;
        while (time < 1)
        {
            time += (Time.deltaTime / Duration);
            float currentState = Tweens.EaseOutExponential(time);
            float currentValue = Mathf.Lerp(0, distance, currentState);
            sprite.size = new Vector2(sprite.size.x, currentValue);
            yield return null;
        }
        sprite.size = new Vector2(sprite.size.x, distance);
        yield return new WaitForSeconds(0.1f);
        CleanUp();
    }

}