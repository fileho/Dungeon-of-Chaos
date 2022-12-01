using System;
using UnityEngine;

public class FollowProjectile : IProjectile
{
    protected float maxSteerForce = 3f;


    protected override void ApplyConfigurations()
    {
        base.ApplyConfigurations();
        FollowProjectileConfiguration _projectileConfiguration = projectileConfiguration as FollowProjectileConfiguration;
        maxSteerForce = _projectileConfiguration.maxSteerForce;
    }


    void FixedUpdate()
    {
        Pursuit(Time.fixedDeltaTime);
    }

    public void Pursuit(float deltaTime)
    {
        float velMagnitude = rb.velocity.magnitude;
        Vector2 futurePosition = (Vector2)GetTarget().transform.position + GetTarget().GetComponent<Rigidbody2D>().velocity * maxSteerForce * deltaTime;
        Vector2 desiredVelocity = (futurePosition - (Vector2)transform.position).normalized * velMagnitude;
        Vector2 steering = desiredVelocity - GetComponent<Rigidbody2D>().velocity;
        Vector2 resultant = (GetComponent<Rigidbody2D>().velocity + steering).normalized * velMagnitude;
        GetComponent<Rigidbody2D>().velocity = resultant;
    }
}


