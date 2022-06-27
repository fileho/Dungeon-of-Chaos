using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float maxMana;
    [SerializeField] private float maxStamina;

    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float dashSpeed = 8f;

    private float hp;
    private float mana;
    private float stamina;


    public static Character instance;

    private TrailRenderer trail;
    private Weapon weapon;
    private new Camera camera;
    private Rigidbody2D rb;

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
        rb = GetComponent<Rigidbody2D>();

        hp = maxHP;
        UpdateHealthBar();
        mana = maxMana;
        UpdateManaBar();
        stamina = maxStamina;
        UpdateStaminaBar();
    }

    void Update()
    {
        RegenerateStamina();
        Dash();
        Attack();
    }

    private void FixedUpdate()
    {
        if (dashing)
            return;
        Move();
    }

    private void RegenerateStamina()
    {
        stamina += 20 * Time.deltaTime;
        stamina = Math.Min(stamina, maxStamina);
        UpdateStaminaBar();
    }

    private void Dash()
    {
        const float dashCost = 20f;

        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        if (stamina < dashCost)
            return;

        stamina -= dashCost;

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

        rb.AddForce(movementSpeed * Time.fixedDeltaTime * 1000 * dir);
    }

    private void Attack()
    {
        Vector2 target = camera.ScreenToWorldPoint(Input.mousePosition);
        weapon.RotateWeapon(target);

        if (!Input.GetMouseButtonDown(0))
            return;

        if (stamina < weapon.GetStaminaCost())
            return;

        stamina -= weapon.GetStaminaCost();
        UpdateStaminaBar();

        weapon.Attack();
    }

    public void TakeDamage(float value)
    {
        hp -= value;
        UpdateHealthBar();

        if (hp <= 0)
            Die();
    }

    private void Die()
    {
        // TODO death screen + respawn
        hp = maxHP;
        UpdateHealthBar();
    }


    private IEnumerator DashAnimation(Vector2 dir)
    {
        trail.enabled = true;
        dashing = true;
        stopDash = false;

        rb.AddForce(dashSpeed * 100 * dir);
        rb.drag = 1;

        float duration = .4f;

        float t = 0;
        while (t < duration && !stopDash)
        {
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        rb.drag = 10;
        dashing = false;
        trail.enabled = false;
    }

    private void UpdateHealthBar()
    {
        UIManager.instance.SetHealthBar(hp / maxHP);
    }
    
    private void UpdateManaBar()
    {
        UIManager.instance.SetManaBar(mana / maxMana);
    }
    private void UpdateStaminaBar()
    {
        UIManager.instance.SetStaminaBar(stamina / maxStamina);
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        stopDash = true;
    }
}
