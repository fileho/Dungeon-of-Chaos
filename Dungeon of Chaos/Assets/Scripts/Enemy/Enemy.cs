using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;


    private EnemyAttack attack;

    private Weapon weapon;

    private bool attacking = false;

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        attack = GetComponentInChildren<EnemyAttack>();
    }

    void Update()
    {
        if (attacking)
            return;
        Move();
        Attack();
        RotateWeapon();
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
        Vector2 dir = (Character.instance.transform.position - transform.position).normalized;
        transform.Translate(movementSpeed * Time.deltaTime * dir);
    }


    public void TakeDamage(float value)
    {
        Debug.Log("AU " + value);

        Vector2 dir = Character.instance.transform.position - transform.position;

        Vector2 n = new Vector2(dir.y, -dir.x).normalized;

        StartCoroutine(TakeDamageAnimation(n));
    }

    private IEnumerator TakeDamageAnimation(Vector2 dir)
    {
        float duration = 0.15f;

        for (int i = 0; i < 4; i++)
        {
            float sign = (i & 0x1) * 2 - 1;

            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                transform.Translate(sign * 2 * Time.deltaTime * dir);
                yield return null;
            }
        }
    }
}
