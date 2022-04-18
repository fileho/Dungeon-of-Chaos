using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float dashSpeed = 8f;

    public static Character instance;

    private TrailRenderer trail;
    
    private Vector2 moveDir = Vector2.up;
    private bool dashing = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        trail.enabled = false;
    }

    void Update()
    {
        if (dashing)
            return;
        Move();
        Dash();
    }

    private void Dash()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        StartCoroutine(DashAnamation(moveDir));
    }

    private void Move()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            dir += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            dir += Vector2.right;
        if (Input.GetKey(KeyCode.W))
            dir += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            dir += Vector2.down;

        dir = dir.normalized;
        if (dir != Vector2.zero)
            moveDir = dir;
        transform.Translate(movementSpeed * Time.deltaTime * dir);
    }

    private IEnumerator DashAnamation(Vector2 dir)
    {
        trail.enabled = true;
        dashing = true;

        float duration = .4f;

        float t = 0;
        while (t < duration)
        {
            transform.Translate(dashSpeed * Time.deltaTime * dir);
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        dashing = false;
        trail.enabled = false;
    }
}
