using System;
using UnityEngine;

public class FollowProjectile : IProjectile
{
    protected float maxSteerForce = 3f;
    protected float lookAhead = 3f;

    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        FollowProjectileConfiguration _projectileConfiguration =
            projectileConfiguration as FollowProjectileConfiguration;
        maxSteerForce = _projectileConfiguration.maxSteerForce;
        lookAhead = _projectileConfiguration.lookAhead;
    }

    void FixedUpdate()
    {
        Pursuit(Time.fixedDeltaTime);
    }

    public void Pursuit(float deltaTime)
    {
        float velMagnitude = rb.velocity.magnitude;
        Vector2 futurePosition = (Vector2)GetTarget().transform.position +
                                 deltaTime * lookAhead * GetTarget().GetComponent<Rigidbody2D>().velocity;
        Vector2 desiredDirection = (futurePosition - (Vector2)transform.position).normalized;

        float rotateAmount = Vector3.Cross(desiredDirection, transform.up).z;
        GetComponent<Rigidbody2D>().angularVelocity = -maxSteerForce * rotateAmount * deltaTime;
        GetComponent<Rigidbody2D>().velocity = transform.up * velMagnitude;
    }
}
