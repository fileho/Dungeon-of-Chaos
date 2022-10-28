using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompIndicator : MeleeIndicator {

    protected SpriteRenderer secondarySprite;


    protected override void InitSprites() {
        base.InitSprites();
        secondarySprite = transform.Find("Secondary").GetComponent<SpriteRenderer>();
    }


    protected override void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration) {
        base.ApplyConfigurations(indicatorConfiguration);
        StompIndicatorConfiguration _indicatorConfiguration = indicatorConfiguration as StompIndicatorConfiguration;
        secondarySprite.color = _indicatorConfiguration.secondarySpriteColor;
        secondarySprite.transform.localScale *= _indicatorConfiguration.secondarySpriteScale;
    }


    protected override IEnumerator ShowIndicator() {
        float time = 0f;
        while (time < Duration) {
            time += Time.deltaTime;
            float t = time / Duration;
            t = (t + 0.3f) * 0.3f;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, t);
            secondarySprite.color = new Color(secondarySprite.color.r, secondarySprite.color.g, secondarySprite.color.b, t);
            yield return null;
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        secondarySprite.color = new Color(secondarySprite.color.r, secondarySprite.color.g, secondarySprite.color.b, 1);
        CleanUp();
    }
}