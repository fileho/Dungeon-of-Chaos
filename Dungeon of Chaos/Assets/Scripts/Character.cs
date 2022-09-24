using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    private IAttack attack;

    public static Character instance;
    private new Camera camera;
    private SkillSystem skillSystem;

    private void Awake()
    {
        instance = this;
    }

    protected override void Init()
    {
        transform.Find("Trail").GetComponent<TrailRenderer>().enabled = false;
        camera = Camera.main;
        skillSystem = FindObjectOfType<SkillSystem>();
        skillSystem.Init(this);
        attack = GetComponentInChildren<IAttack>();
        SaveSystem.instance.save.MoveCharacter();
    }

    protected override void CleanUp()
    {
        // TODO respawn logic
        SaveSystem.instance.save.LoadLevel();
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


    public override Vector2 GetTargetPosition() {
        return camera.ScreenToWorldPoint(Input.mousePosition); ;
    }

    private void UpdateCooldowns()
    {
        skillSystem.UpdateCooldowns();
    }

    private void UseSkills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            skillSystem.UseSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            skillSystem.UseSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            skillSystem.Dash(movement.GetMoveDir());
        }
    }


    private void FixedUpdate()
    {
        if (skillSystem.IsDashing())
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

        Vector2 dir = GetTargetPosition() - (Vector2)transform.position;

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
        skillSystem.DashCollision(col);
    }
}
