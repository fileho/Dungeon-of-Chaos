using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    private IAttack attack;

    public static Character instance;
    private new Camera camera;

    private void Awake()
    {
        instance = this;
    }

    protected override void Init()
    {
        transform.Find("Trail").GetComponent<TrailRenderer>().enabled = false;
        camera = Camera.main;
        (SkillSystem.instance.GetDash().GetCurrentSkill() as IDashSkill).Init(this);
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
        if (SkillSystem.instance == null || SkillSystem.instance.GetActivatedSkills() == null || !SkillSystem.instance.HasActivatedSkill())
            return;
        foreach (var skill in SkillSystem.instance.GetActivatedSkills())
        {
            skill.GetCurrentSkill().UpdateCooldown();
        }
        SkillSystem.instance.GetDash().GetCurrentSkill().UpdateCooldown();
        if (SkillSystem.instance.GetSecondary() != null)
            SkillSystem.instance.GetSecondary().GetCurrentSkill().UpdateCooldown();

    }

    private void UseSkills()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            (SkillSystem.instance.GetDash().GetCurrentSkill() as IDashSkill).Use(this, null, new List<Vector2>() { movement.GetMoveDir() });
        }
    }

    private void UseSkill(int index)
    {
        SkillInfoActive skill = SkillSystem.instance.GetActivatedSkills()[index];
        if (!skill)
            return;
        skill.GetCurrentSkill().Use(this);
    }

    private void FixedUpdate()
    {
        if (SkillSystem.instance != null && (SkillSystem.instance.GetDash().GetCurrentSkill() as IDashSkill).IsDashing())
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
        (SkillSystem.instance.GetDash().GetCurrentSkill() as IDashSkill).TriggerCollision(col);
    }
}
