using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public Transform target { get; private set; }

    private float nextWaypointDistance = 5f;
    private int currentWaypoint = 0;
    private float speed = 10f;
    //private bool reachedEndOfPath = false;
    private Path path;
    private Seeker seeker;
    private Rigidbody2D rb;

    public void Init(float l_speed, Rigidbody2D l_rb, float l_nextWayPointDistance)
    {
        seeker = GetComponent<Seeker>();
        speed = l_speed;
        rb = l_rb;
        nextWaypointDistance = l_nextWayPointDistance;
    }

    public void UpdatePath(Transform l_target)
    {
        target = l_target;
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }

    public void UpdateMovement(Vector2 extraForce) // call this in fixed update
    {
        if (path == null || currentWaypoint >= path.vectorPath.Count)
            return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * rb.mass * 1000f * Time.fixedDeltaTime;

        rb.AddForce(force + extraForce);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
