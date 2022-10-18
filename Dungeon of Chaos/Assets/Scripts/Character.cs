using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    private IAttack attack;

    public static Character instance;
    private new Camera camera;
    public SkillSystem SkillSystem { get; private set; }
    private GameController gameController;

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

    protected override void CleanUp()
    {
        // TODO respawn logic
        gameController.Death();
    }

    void Update()
    {
        if (dead)
            return;
        RegenerateStamina();
        RotateWeapon();
        Attack();
        FlipSprite();
        UseSkills();
        UpdateCooldowns();
    }

    public override Vector2 GetTargetPosition()
    {
        return camera.ScreenToWorldPoint(Input.mousePosition);
        ;
    }

    public override Vector2 GetTargetDirection() {
        return (GetTargetPosition() - (Vector2)transform.position);
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
        if (SkillSystem.IsDashing())
            return;
        Move();
    }

    private void RegenerateStamina()
    {
        stats.RegenerateStamina(stats.GetStaminaRegen() * Time.deltaTime);
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

    private void Move()
    {
        movement.Move(footstepsSFX);
    }

    private void RotateWeapon()
    {
        weapon.RotateWeapon(GetTargetPosition());
    }

    private void Attack()
    {
        if (!Input.GetMouseButtonDown(0) || attack.IsAttacking())
            return;

        float staminaCost = attack.GetStaminaCost();
        if (!stats.HasStamina(staminaCost))
            return;

        stats.ConsumeStamina(staminaCost);
        attack.Attack();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        SkillSystem.DashCollision(col);
    }
}
