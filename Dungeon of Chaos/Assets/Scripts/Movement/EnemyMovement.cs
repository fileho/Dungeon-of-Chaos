using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Movement/MeleeEnemy")]
public class EnemyMovement : IMovement
{
    private AIAgent agent;
    private Rigidbody2D rb;

    [Header("Separation")]
    [SerializeField]
    private float separationForce = 1f;
    [SerializeField]
    private float separationWeight = 1f;
    [SerializeField]
    private float separationDistance = 1.25f;
    [SerializeField]
    private float separationDetectionRadius = 7.5f;

    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private LayerMask wallLayer;

    private const int maxEvadeTargets = 5;
    [Header("Layers")]
    private Collider2D[] evadeEnemyHits = new Collider2D[maxEvadeTargets];
    private Rigidbody2D targetRB;

    public Collider2D collider { get; private set; }

    public override IMovement Init(Transform transform, Stats stats)
    {
        agent = transform.GetComponent<AIAgent>();
        rb = transform.GetComponent<Rigidbody2D>();
        targetRB = Character.instance.GetComponent<Rigidbody2D>();
        collider = transform.GetComponent<Collider2D>();
        if (agent != null)
        {
            float nextWaypointDistance = transform.GetComponent<AttackManager>().GetMinimumAttackRange() / 2f;
            agent.Init(stats.MovementSpeed(), rb, nextWaypointDistance);
        }

        return this;
    }

    public override void Move()
    {
        // Uses the A* pathfinding
        agent.UpdatePath(targetRB.transform);

        Vector2 separation = Vector2.zero;
        int separationHitsCount =
            Physics2D.OverlapCircleNonAlloc(rb.position, separationDetectionRadius, evadeEnemyHits, obstacleLayer);
        if (separationHitsCount > 1) // it collides with self also
            separation = Helper.AutonomousAgent.SeparationGetSteering(collider, evadeEnemyHits, separationHitsCount,
                                                                      separationDistance, separationForce) *
                         separationWeight;
        agent.UpdateMovement(separation);
    }

    public override void MuteSfx()
    {
    }

    public override Vector2 GetMoveDir()
    {
        return Vector2.zero;
    }
}
