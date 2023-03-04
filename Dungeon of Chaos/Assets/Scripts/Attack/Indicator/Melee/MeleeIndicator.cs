using System.Collections;
using UnityEngine;

public abstract class MeleeIndicator : IIndicator
{
    protected override void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration)
    {
        base.ApplyConfigurations(indicatorConfiguration);
        MeleeIndicatorConfiguration _indicatorConfiguration = indicatorConfiguration as MeleeIndicatorConfiguration;
        sprite.color = _indicatorConfiguration.color;
        sprite.transform.localScale *= _indicatorConfiguration.scale;
    }
}
