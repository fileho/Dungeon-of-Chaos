using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialIndicator : IIndicator
{
    private float range;

    // To make the radius of the effect equal to the range
    private float factor;
    protected override void InitSprites()
    {
    }

    protected override void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration)
    {
        Duration = indicatorConfiguration.duration;
        SpecialIndicatorConfiguration _indicatorConfiguration = indicatorConfiguration as SpecialIndicatorConfiguration;
        range = _indicatorConfiguration.range;
        factor = _indicatorConfiguration.factor;
    }

    protected override IEnumerator ShowIndicator()
    {
        float time = 0f;
        while (time < 1)
        {
            time += (Time.deltaTime / Duration);
            float currentState = Tweens.EaseOutExponential(time);
            float currentValue = Mathf.Lerp(0, range * factor, currentState);
            transform.localScale = Vector3.one * currentValue;
            yield return null;
        }
        transform.localScale = range * factor * Vector3.one;
        yield return new WaitForSeconds(0.1f);
        CleanUp();
    }
}
