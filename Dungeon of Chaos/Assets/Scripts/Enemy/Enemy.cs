using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private ILoot loot;

    private EnemyAttack attack;

    private bool attacking = false;

    protected override void Init()
    {
        attack = GetComponentInChildren<EnemyAttack>();
        loot = Instantiate(loot).Init(transform);
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

    protected override void Die()
    {
        loot.Drop();
        Destroy(transform.parent.gameObject);
    }
}
