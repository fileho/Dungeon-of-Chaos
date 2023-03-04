using System.Collections.Generic;
using UnityEngine;

namespace Helper
{
public class AutonomousAgent
{
    public static Vector3 Seek(Rigidbody2D selfRB, Vector3 targetPosition, float maxSeekAccel)
    {
        /* Get the direction */
        Vector3 acceleration = (targetPosition - selfRB.transform.position);
        acceleration.Normalize();

        /* Accelerate to the target */
        acceleration *= maxSeekAccel;
        return acceleration;
    }

    public static void Steer(Rigidbody2D selfRB, Vector2 linearAcceleration, float steerForce)
    {
        selfRB.velocity += (linearAcceleration * steerForce * Time.deltaTime);

        if (selfRB.velocity.magnitude > steerForce)
        {
            selfRB.velocity = selfRB.velocity.normalized * steerForce;
        }
    }

    public static Vector3 PursueGetSteering(Rigidbody2D selfRB, Rigidbody2D target, float lookAheadDistance,
                                            float moveSpeed)
    {
        /* Calculate the distance to the target */
        Vector3 displacement = target.position - selfRB.position;
        float distance = displacement.magnitude;

        /* Get the character's speed */
        float speed = selfRB.velocity.magnitude;

        /* Calculate the prediction time */
        float prediction;
        if (speed <= distance / lookAheadDistance)
        {
            prediction = lookAheadDistance;
        }
        else
        {
            prediction = distance / speed;
        }

        /* Put the target together based on where we think the target will be */
        Vector3 explicitTarget = target.position + target.velocity * prediction;
        return Seek(selfRB, explicitTarget, moveSpeed);
    }
    public static Vector3 SeparationGetSteering(Collider2D selfCollider, Collider2D[] targets, int targetsCount,
                                                float maxSepDist, float sepMaxAcceleration)
    {
        Vector3 acceleration = Vector3.zero;
        float selfColliderSize = GetColliderSize(selfCollider);

        for (int i = 0; i < targetsCount; i++)
        {
            float colliderSize = GetColliderSize(targets[i]);

            /* Get the direction and distance from the target */
            Vector3 direction = selfCollider.transform.position - targets[i].transform.position;
            float dist = direction.magnitude;

            if (dist < maxSepDist)
            {
                /* Calculate the separation strength (can be changed to use inverse square law rather than linear) */
                var strength =
                    sepMaxAcceleration * (maxSepDist - dist) / (maxSepDist - selfColliderSize - colliderSize);

                direction.Normalize();
                acceleration += (direction * strength);
            }
        }
        return acceleration;
    }

    public static Vector3 EvadeGetSteering(Rigidbody2D selfRB, Rigidbody2D target, float maxPrediction, float maxAccel,
                                           float panicDist, bool decelerateOnStop, float timeToTarget)
    {
        /* Calculate the distance to the target */
        Vector3 displacement = target.position - selfRB.position;
        float distance = displacement.magnitude;

        /* Get the target's speed */
        float speed = target.velocity.magnitude;

        /* Calculate the prediction time */
        float prediction;
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
            // Place the predicted position a little before the target reaches the character
            prediction *= 0.9f;
        }

        /* Put the target together based on where we think the target will be */
        Vector3 explicitTarget = target.position + target.velocity * prediction;

        return FleeGetSteering(selfRB, explicitTarget, maxAccel, panicDist, decelerateOnStop, timeToTarget);
    }

    public static Vector3 EvadeGetSteering(Rigidbody2D selfRB, Vector2 targetPos, float maxPrediction, float maxAccel,
                                           float panicDist, bool decelerateOnStop, float timeToTarget)
    {
        /* Calculate the distance to the target */
        Vector3 displacement = targetPos - selfRB.position;
        float distance = displacement.magnitude;

        /* Get the target's speed */
        float speed = 0;

        /* Calculate the prediction time */
        float prediction;
        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        }
        else
        {
            prediction = distance / speed;
            // Place the predicted position a little before the target reaches the character
            prediction *= 0.9f;
        }

        /* Put the target together based on where we think the target will be */
        Vector3 explicitTarget = targetPos * prediction;
        return FleeGetSteering(selfRB, explicitTarget, maxAccel, panicDist, decelerateOnStop, timeToTarget);
    }

    public static Vector3 FleeGetSteering(Rigidbody2D selfRB, Vector3 targetPosition, float maxAcceleration,
                                          float panicDist, bool decelerateOnStop, float timeToTarget)
    {
        /* Get the direction */
        Vector3 acceleration = selfRB.transform.position - targetPosition;

        /* If the target is far way then don't flee */
        if (acceleration.magnitude > panicDist)
        {
            /* Slow down if we should decelerate on stop */
            if (decelerateOnStop && selfRB.velocity.magnitude > 0.001f)
            {
                /* Decelerate to zero velocity in time to target amount of time */
                acceleration = -selfRB.velocity / timeToTarget;

                if (acceleration.magnitude > maxAcceleration)
                {
                    acceleration = GiveMaxFleeAccel(acceleration, maxAcceleration);
                }

                return acceleration;
            }
            else
            {
                selfRB.velocity = Vector3.zero;
                return Vector3.zero;
            }
        }

        return GiveMaxFleeAccel(acceleration, maxAcceleration);
    }

    public static Vector3 GiveMaxFleeAccel(Vector3 v, float maxFleeAccel)
    {
        v.Normalize();

        v *= maxFleeAccel;
        return v;
    }

    public static float GetColliderSize(Collider2D collider)
    {
        float targetColliderSize = 1;

        if (collider is BoxCollider2D bCol)
        {
            targetColliderSize = bCol.size.x > bCol.size.y ? bCol.size.x : bCol.size.y;
        }
        else if (collider is CapsuleCollider2D cCol)
        {
            targetColliderSize = cCol.size.x;
        }
        else if (collider is CircleCollider2D cirCol)
        {
            targetColliderSize = cirCol.radius;
        }
        return targetColliderSize;
    }
}
}
