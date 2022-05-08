using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float swipe = 10f;
    [SerializeField] private float dmg = 10f;
    [SerializeField] private float range = 1f;
    [Range(0, 1)]
    [SerializeField] private float rotationSpeed = 1f;


    private TrailRenderer trail;

    private new BoxCollider2D collider;
    private bool attacking = false;
    private List<GameObject> hitEnemies;

    void Start()
    {
        hitEnemies = new List<GameObject>();
        trail = GetComponentInChildren<TrailRenderer>();
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        trail.gameObject.SetActive(false);
    }

    void Update()
    {
        if (attacking)
            return;
        transform.localPosition = Vector3.back;
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

        transform.up = Vector3.Lerp(transform.up, dir, rotationSpeed * 0.1f);
    }

    private IEnumerator AttackAnimation(float s, float d, float r)
    {
        // Setup
        attacking = true;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + transform.up * r;
        trail.gameObject.SetActive(true);
        collider.enabled = true;
        var rot = transform.localRotation;
        hitEnemies.Clear();

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

    public void HammerAttack(float duration)
    {
        StartCoroutine(ExecuteHammerAttack(duration));
    }

    private IEnumerator ExecuteHammerAttack(float duration)
    {
        float t = 0;

        Vector2 startPoint = transform.up;

        Vector2 endPoint = -transform.right;

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


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            Debug.Log("Player hit");

        if (!col.CompareTag("Enemy"))
            return;
        Enemy enemy = col.GetComponent<Enemy>();
        if (hitEnemies.Contains(enemy.gameObject))
            return;
        hitEnemies.Add(enemy.gameObject);
        enemy.TakeDamage(DealDamage());
    }
}
