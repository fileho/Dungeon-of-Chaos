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
        movement.Move();
    }
}
