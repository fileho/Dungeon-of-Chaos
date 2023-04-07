using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : Unit
{
    private IAttack attack;

    public static Character instance;
    private new Camera camera;
    public SkillSystem SkillSystem { get; private set; }
    private GameController gameController;

    private int blockedInput = 0;
    private const float maxBiteCooldown = 0.75f;
    private float biteCooldown = 0f;

    private void Awake()
    {
        instance = this;
        SkillSystem = FindObjectOfType<SkillSystem>();
    }

    protected override void Init()
    {
        gameController = FindObjectOfType<GameController>();
        transform.Find("Trail").GetComponent<TrailRenderer>().enabled = false;
        camera = Camera.main;
        SkillSystem.Init(this);
        attack = GetComponentInChildren<IAttack>();
    }

    protected override void Die()
    {
        if (SkillSystem.ShouldResurrect())
        {
            Debug.Log("Resurrect");
            SkillSystem.Resurrect();
            return;
        }

        base.Die();
    }

    protected override void CleanUp()
    {
        gameController.Death();
    }

    void Update()
    {
        if (dead)
            return;

        if (IsInputBlocked())
            return;
        RegenerateStamina();
        RegenerateMana();
        RotateWeapon();
        Attack();
        FlipSprite();
        UseSkills();
        UpdateCooldowns();
        UpdateBite();
    }

    private void UpdateBite()
    {
        if (biteCooldown > 0)
            biteCooldown -= Time.deltaTime;
    }

    public override Vector2 GetTargetPosition()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public override Vector2 GetTargetDirection()
    {
        return (GetTargetPosition() - (Vector2)transform.position);
    }

    public void BlockInput()
    {
        ++blockedInput;
        movement.MuteSfx();
    }

    public void UnblockInput()
    {
        --blockedInput;
    }

    public bool IsInputBlocked()
    {
        return blockedInput > 0;
    }

    private void UpdateCooldowns()
    {
        SkillSystem.UpdateCooldowns();
    }

    private void UseSkills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SkillSystem.UseSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SkillSystem.UseSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SkillSystem.UseSkill(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillSystem.UseSkill(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillSystem.UseSkill(4);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkillSystem.Dash(movement.GetMoveDir());
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            SkillSystem.SecondaryAttack();
        }
    }

    private void FixedUpdate()
    {
        if (dead)
            return;

        if (IsInputBlocked())
            return;

        if (SkillSystem.IsDashing())
            return;
        Move();
    }

    private void RegenerateStamina()
    {
        if (stats.ShouldRegenerateStamina())
            stats.RegenerateStamina(stats.GetStaminaRegen() * Time.deltaTime);
    }

    private void RegenerateMana()
    {
        stats.RegenerateMana(stats.GetManaRegen() * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (attack.IsAttacking())
            return;

        Vector2 dir = GetTargetDirection();
        if (dir.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = Vector3.one;
    }

    public bool IsAttacking()
    {
        return attack.IsAttacking();
    }

    private void Move()
    {
        movement.Move();
    }

    private void RotateWeapon()
    {
        if (attack.IsAttacking())
            return;
        weapon.RotateWeapon(GetTargetPosition());
    }

    private void Attack()
    {
        if (!Input.GetMouseButtonDown(0) || attack.IsAttacking() || SkillSystem.IsAttacking())
            return;

        float staminaCost = attack.GetStaminaCost();
        if (!stats.HasStamina(staminaCost))
        {
            InGameUIManager.instance.NotEnoughStamina();
            return;
        }

        stats.ConsumeStamina(staminaCost);
        attack.Attack();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        SkillSystem.DashCollision(col);

        if (biteCooldown > 0 || SkillSystem.IsAttackDashing())
            return;
        var e = col.transform.GetComponent<Enemy>();
        if (e == null)
            return;
        TakeDamage(3 + e.stats.GetPhysicalDamage() / 8);
        biteCooldown = maxBiteCooldown;
    }
}
