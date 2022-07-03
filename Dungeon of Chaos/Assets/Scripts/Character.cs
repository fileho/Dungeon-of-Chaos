using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField] private List<Skill> skills;
    [SerializeField] private Dash dash;


    public static Character instance;
    private new Camera camera;

    private Vector2 moveDir = Vector2.up;

    private void Awake()
    {
        instance = this;
    }

    protected override void Init()
    {
        transform.Find("Trail").GetComponent<TrailRenderer>().enabled = false;
        camera = Camera.main;

        dash.ResetDash();
        stats.ResetStats();
        UpdateHealthBar();
        UpdateManaBar();
        UpdateStaminaBar();
    }

    protected override void TakeDamageSideEffect()
    {
        UpdateHealthBar();
    }

    protected override void Die()
    {
        // TODO respawn logic
        stats.ResetStats();
    }

    void Update()
    {
        RegenerateStamina();
        Dash();
        RotateWeapon();
        Attack();
        FlipSprite();
        UseSkills();
    }

    private void UseSkills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skills[0].GetComponent<IActiveSkill>().Use();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            skills[1].GetComponent<IActiveSkill>().Use();
        }
        UpdateManaBar();
    }

    private void FixedUpdate()
    {
        if (dash.IsDashing())
            return;
        Move();
    }

    private void RegenerateStamina()
    {
        stats.RegenerateStamina(20 * Time.deltaTime);
        UpdateStaminaBar();
    }

    private void FlipSprite()
    {
        if (rb.velocity.x < 0.01f)
            sprite.flipX = true;
        if (rb.velocity.x > 0.01f)
            sprite.flipX = false;
    }

    private void Dash()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || dash.IsDashing())
            return;

        float dashCost = dash.Cost();
        if (!stats.HasStamina(dashCost))
            return;

        stats.ConsumeStamina(dashCost);
        dash.StartDash(moveDir);
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

        rb.AddForce(stats.MovementSpeed() * Time.fixedDeltaTime * 1000 * dir);
    }

    private void RotateWeapon()
    {
        Vector2 target = camera.ScreenToWorldPoint(Input.mousePosition);
        weapon.RotateWeapon(target);
    }

    private void Attack()
    {
        if (!Input.GetMouseButtonDown(0) || weapon.IsAttacking())
            return;

        float staminaCost = weapon.GetStaminaCost();
        if (!stats.HasStamina(staminaCost))
            return;

        stats.ConsumeStamina(staminaCost);
        UpdateStaminaBar();

        weapon.Attack();
    }

    private void UpdateHealthBar()
    {
        UIManager.instance.SetHealthBar(stats.HpRatio());
    }
    
    private void UpdateManaBar()
    {
        UIManager.instance.SetManaBar(stats.ManaRatio());
    }
    private void UpdateStaminaBar()
    {
        UIManager.instance.SetStaminaBar(stats.StaminaRatio());
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        dash.OnCollisionEnter2D(col);
    }
}
