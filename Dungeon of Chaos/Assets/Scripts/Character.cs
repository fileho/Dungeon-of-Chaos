using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private float maxMana;
    [SerializeField] private float maxStamina;

    [SerializeField] private float movementSpeed = 3f;

    private float hp;
    private float mana;
    private float stamina;

    [SerializeField] private List<Skill> skills;
    [SerializeField] private Dash dash;


    public static Character instance;
    private Weapon weapon;
    private new Camera camera;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Vector2 moveDir = Vector2.up;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        transform.Find("Trail").GetComponent<TrailRenderer>().enabled = false;
        weapon = GetComponentInChildren<Weapon>();
        camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

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
    }

    private void FixedUpdate()
    {
        if (dash.IsDashing())
            return;
        Move();
    }

    private void RegenerateStamina()
    {
        stamina += 20 * Time.deltaTime;
        stamina = Math.Min(stamina, maxStamina);
        UpdateStaminaBar();
    }

    private void FlipSprite()
    {
        if (rb.velocity.x < 0.01f)
            sprite.flipX = false;
        if (rb.velocity.x > 0.01f)
            sprite.flipX = true;
    }

    private void Dash()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || dash.IsDashing())
            return;

        float dashCost = dash.Cost();
        if (stamina < dashCost)
            return;

        stamina -= dashCost;
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

        rb.AddForce(movementSpeed * Time.fixedDeltaTime * 1000 * dir);
    }

    private void RotateWeapon()
    {
        Vector2 target = camera.ScreenToWorldPoint(Input.mousePosition);
        weapon.RotateWeapon(target);
    }

    private void Attack()
    {
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

    public void RestoreHealth(float value)
    {
        hp += value;
        hp = Mathf.Min(hp, maxHP);
        UpdateHealthBar();
    }

    private void Die()
    {
        // TODO death screen + respawn
        hp = maxHP;
        UpdateHealthBar();
    }

    public void ConsumeMana(float value)
    {
        mana -= value;
        UpdateManaBar();
    }

    public float GetMana()
    {
        return mana;
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
        dash.OnCollisionEnter2D(col);
    }
}
