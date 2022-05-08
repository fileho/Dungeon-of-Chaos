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
    private Weapon weapon;
    private new Camera camera;

    private Vector2 moveDir = Vector2.up;
    private bool dashing = false;

    private bool stopDash = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        trail = transform.Find("Trail").GetComponent<TrailRenderer>();
        trail.enabled = false;
        weapon = GetComponentInChildren<Weapon>();
        camera = Camera.main;
    }

    void Update()
    {
        if (dashing)
            return;
        Move();
        Dash();
        Attack();
    }

    private void Dash()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        StartCoroutine(DashAnimation(moveDir));
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

    private void Attack()
    {
        Vector2 target = camera.ScreenToWorldPoint(Input.mousePosition);
        weapon.RotateWeapon(target);

        if (!Input.GetMouseButtonDown(0))
            return;

        weapon.Attack();
    }

    private IEnumerator DashAnimation(Vector2 dir)
    {
        trail.enabled = true;
        dashing = true;
        stopDash = false;

        float duration = .4f;

        float t = 0;
        while (t < duration && !stopDash)
        {
            transform.Translate(dashSpeed * Time.deltaTime * dir);
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        dashing = false;
        trail.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        stopDash = true;
    }
}
