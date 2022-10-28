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
        sprite.material.SetFloat("_Arc1", 180f - sweep/2f);
        sprite.material.SetFloat("_Arc2", 180f - sweep/2f);
    }
}