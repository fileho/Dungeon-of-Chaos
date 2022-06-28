using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float movementSpeed = 2f;

    private float HP;

    private Rigidbody2D rb;
    private EnemyAttack attack;

    private Weapon weapon;

    private bool attacking = false;

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        attack = GetComponentInChildren<EnemyAttack>();
        rb = GetComponent<Rigidbody2D>();

        HP = maxHP;
    }

    void Update()
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
            // rb.velocity = Vector2.zero;
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
        
        rb.AddForce(movementSpeed * Time.fixedDeltaTime * 1000 * dir);
    }


    public void TakeDamage(float value)
    {
        HP -= value;
        if (HP <= 0)
            Die();

        Vector2 dir = (transform.position - Character.instance.transform.position).normalized;

        rb.AddForce(dir * 500);

        Vector2 n = new Vector2(dir.y, -dir.x).normalized;

        StartCoroutine(TakeDamageAnimation(n));
    }

    private void Die()
    {
        Destroy(gameObject);
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
