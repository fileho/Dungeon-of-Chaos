using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float swipe = 10f;
    [SerializeField] private float dmg = 10f;
    [SerializeField] private float range = 1f;
    [SerializeField] private float staminaCost = 20f;
    [Range(0, 1)]
    [SerializeField] private float rotationSpeed = 1f;
    [Tooltip("Initial offset z angle for weapon to point right")]
    [SerializeField] private float angleOffset = 0f;


    private TrailRenderer trail;
    private new BoxCollider2D collider;
    private bool attacking = false;
    private List<GameObject> hitUnits;

    void Start()
    {
        hitUnits = new List<GameObject>();
        trail = GetComponentInChildren<TrailRenderer>();
        // sprite = GetComponent<SpriteRenderer>();
        collider = GetComponentInChildren<BoxCollider2D>();
        collider.enabled = false;
        trail.gameObject.SetActive(false);
    }


    public void Attack()
    {
        Attack(swipe, dmg, range);
    }

    public void Attack(float s, float d, float r)
    {
        if (attacking)
            return;
        StartCoroutine(AttackAnimation(s, d, r));
    }


    public float DealDamage()
    {
        return dmg;
    }

    public void RotateWeapon(Vector2 target)
    {
        if (attacking)
            return;

        Vector2 dir = (target - (Vector2)transform.position).normalized;

        if (transform.lossyScale.x > 0)
            dir *= -1;


        Vector3 rotated = Quaternion.Euler(0, 0, 90) * dir;

        var q = Quaternion.LookRotation(Vector3.forward, rotated);

        var e = q.eulerAngles;
        e.z += transform.lossyScale.x > 0 ? - angleOffset : angleOffset;

        transform.rotation = Quaternion.Euler(e);
    }


    private Vector3 GetForwardDirection()
    {
        float dir = transform.lossyScale.x > 0 ? 1 : -1;

        float a =  angleOffset + transform.rotation.eulerAngles.z * dir;
        a *= Mathf.Deg2Rad;
        // Vector3.R


        return new Vector3(Mathf.Cos(a), Mathf.Sin(a), 0);
    }

    private IEnumerator AttackAnimation(float s, float d, float r)
    {
        // Setup
        attacking = true;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos - GetForwardDirection() * r;
        trail.gameObject.SetActive(true);
        collider.enabled = true;
        var rot = transform.localRotation;
        hitUnits.Clear();

        float time = 0;
        float duration = .6f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = Mathf.Clamp01(time / duration);
            transform.localPosition = Vector3.Lerp(startPos, endPos, t * (1 - t) * 4);

            float setup = 0.2f;
            if (t < setup)
                transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z - s), t / setup);
            else if (1 - t < setup)
                transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z + s), (1 - t) / setup);
            else
            {
                transform.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(0, 0, rot.eulerAngles.z - s),
                    Quaternion.Euler(0, 0, rot.eulerAngles.z + s), 
                    (t - setup) / (1 - 2 * setup));
            }
            yield return null;
        }

        // Restore settings
        collider.enabled = false;
        trail.gameObject.SetActive(false);
        transform.localRotation = rot;
        attacking = false;
    }

    public float GetStaminaCost()
    {
        return staminaCost;
    }

    public void HammerAttack(float duration)
    {
        StartCoroutine(ExecuteHammerAttack(duration));
    }

    private IEnumerator ExecuteHammerAttack(float duration)
    {
        float t = 0;

        Vector2 startPoint = transform.up;

        Vector2 endPoint = -transform.right;
        hitUnits.Clear();

        float d = duration * 0.8f;

        while (t < d)
        {
            t += Time.deltaTime;
            transform.up = Vector2.Lerp(startPoint, endPoint, t / d);
            yield return null;
        }

        t = 0;
        d = duration * 0.2f;

        while (t < d)
        {
            t += Time.deltaTime;
            transform.up = Vector2.Lerp(endPoint, startPoint,t / d);
            yield return null;
        }
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    public void PlayerHit()
    {
        if (hitUnits.Contains(Character.instance.gameObject))
            return;

        hitUnits.Add(Character.instance.gameObject);

        Character.instance.TakeDamage(DealDamage());
    }

    public void EnemyHit(Enemy enemy)
    {
        if (hitUnits.Contains(enemy.gameObject))
            return;
        hitUnits.Add(enemy.gameObject);
        enemy.TakeDamage(DealDamage());
    }



    public void SetAttacking(bool value)
    {
        attacking = value;
    }
}
