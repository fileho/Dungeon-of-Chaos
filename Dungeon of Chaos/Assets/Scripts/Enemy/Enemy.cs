using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Enemy : Unit
{
    private EnemyAttack attack;

    private bool attacking = false;

    protected override void Init()
    {
        CreateUniqueStats();
        attack = GetComponentInChildren<EnemyAttack>();
    }

    protected override void TakeDamageSideEffect()
    {
        Vector2 dir = (transform.position - Character.instance.transform.position).normalized;
        rb.AddForce(dir * 500);
        Vector2 n = new Vector2(dir.y, -dir.x).normalized;

        StartCoroutine(TakeDamageAnimation(n));
    }

    // Each enemy has its unique stats instance so it can be modified
    private void CreateUniqueStats()
    {
        stats = Instantiate(stats);
        stats.ResetStats();
    }


    private void Update()
    {
        if (attacking)
            return;
        Attack();
        RotateWeapon();
    }

    private void FixedUpdate()
    {
        if (attacking)
        {
            return;
        }
      
        Move();
    }

    public void AttackWeapon(float swipe, float dmg, float range)
    {
        weapon.Attack(swipe, dmg, range);
    }

    private void RotateWeapon()
    {
        if (weapon != null)
        {
            weapon.RotateWeapon(Character.instance.transform.position);
        }
    }

    private void Attack()
    {
        if (!attack.CanUse(transform.position))
            return;


        attacking = true;
        attack.Use();

        Invoke(nameof(ReadyAttack), attack.GetDelay());
    }

    private void ReadyAttack()
    {
        attacking = false;
    }

    private void Move()
    {
        // TODO Add pathfinding
        Vector2 dir = (Character.instance.transform.position - transform.position).normalized;
        
        rb.AddForce(stats.MovementSpeed() * Time.fixedDeltaTime * 1000 * dir);
    }


    private IEnumerator TakeDamageAnimation(Vector2 dir)
    {
        float duration = 0.15f;

        for (int i = 0; i < 4; i++)
        {
            float sign = (i & 0x1) * 2 - 1;
            rb.AddForce(sign * 200 * dir);
            yield return new WaitForSeconds(duration);
        }
    }
}
