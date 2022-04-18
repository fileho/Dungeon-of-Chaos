using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float swipe = 10f;
    [SerializeField] private float dmg = 10f;
    [SerializeField] private float range = 1f;


    private TrailRenderer trail;
    private new Camera camera;
    private new BoxCollider2D collider;
    private bool attacking = false;
    private List<GameObject> hitEnemies;

    void Start()
    {
        hitEnemies = new List<GameObject>();
        camera = Camera.main;
        trail = GetComponentInChildren<TrailRenderer>();
        collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        trail.gameObject.SetActive(false);
    }

    void Update()
    {
        if (attacking)
            return;
        Attack();
        RotateWeapon();
        transform.localPosition = Vector3.back;
    }

    private void Attack()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        StartCoroutine(AttackAnimation());
    }


    public float DealDamage()
    {
        return dmg;
    }

    private void RotateWeapon()
    {
        Vector2 wp = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (wp - (Vector2)transform.position).normalized;

        transform.up = Vector3.Lerp(transform.up, dir, 0.1f);
    }

    private IEnumerator AttackAnimation()
    {
        // Setup
        attacking = true;
        Vector3 startPos = transform.localPosition;
        Vector3 endPos = startPos + transform.up * range;
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
                transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z - swipe), t / setup);
            else if (1 - t < setup)
                transform.localRotation = Quaternion.Lerp(rot, Quaternion.Euler(0, 0, rot.eulerAngles.z + swipe), (1 - t) / setup);
            else
            {
                transform.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(0, 0, rot.eulerAngles.z - swipe),
                    Quaternion.Euler(0, 0, rot.eulerAngles.z + swipe), 
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy"))
            return;
        Enemy enemy = col.GetComponent<Enemy>();
        if (hitEnemies.Contains(enemy.gameObject))
            return;
        hitEnemies.Add(enemy.gameObject);
        enemy.TakeDamage(DealDamage());
    }
}
