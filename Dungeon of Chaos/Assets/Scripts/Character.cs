using System.Collections.Generic;
using UnityEngine;

public class Character : Unit
{
    [SerializeField] private List<Skill> skills;
    [SerializeField] private Dash dash;


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

        dash = Instantiate(dash).Init(stats);
    }

    protected override void Die()
    {
        // TODO respawn logic
        SaveSystem.instance.save.LoadLevel();
        // stats.ResetStats();
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
        stats.RegenerateStamina(20 * Time.deltaTime);
    }

    private void FlipSprite()
    {
        if (weapon.IsAttacking())
            return;

        Vector2 dir = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (dir.x > 0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (dir.x < -0.01f)
            transform.localScale = Vector3.one;
    }

    private void Dash()
    {
        dash.StartDash(movement.GetMoveDir());
    }

    private void Move()
    {
        movement.Move();
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
        weapon.Attack();
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        dash.OnCollisionEnter2D(col);
    }
}
