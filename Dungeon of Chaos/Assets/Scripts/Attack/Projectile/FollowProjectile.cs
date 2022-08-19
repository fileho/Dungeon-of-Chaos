using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : IProjectile
{
    private float homingStrength;

    protected override void ApplyConfigurations() {
        base.ApplyConfigurations();
        homingStrength = (projectileConfiguration as FollowProjectileConfiguration).homingStrength;
    }


    void FixedUpdate() {
        rb.drag = 0.5f;
        Vector2 dir = GetTargetPosition() - (Vector2)transform.position;
        dir.Normalize();
        rb.AddForce(homingStrength * Time.fixedDeltaTime * dir);
    }

}
