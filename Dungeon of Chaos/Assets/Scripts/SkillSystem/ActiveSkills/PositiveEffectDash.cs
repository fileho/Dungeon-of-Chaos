using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Dash/Positive Effect Dash")]
public class PositiveEffectDash : Dash
{
    public override void Use(Vector2 dir)
    {
        base.Use(dir);
        foreach (var effect in effects)
            effect.Use(owner, new List<Unit>() { owner });
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (!dashing)
            return;

        stopDash = true;
    }
}
