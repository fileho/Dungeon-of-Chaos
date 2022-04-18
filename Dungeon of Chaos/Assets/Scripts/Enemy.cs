using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f;

    [SerializeField] private EnemyAttack attack;

    private float cooldown = 0;


    private bool attacking = false;

    void Update()
    {
        if (attacking)
            return;
        Move();
        UpdateCooldown();
        Attack();
    }

    private void UpdateCooldown()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
    }

    private void Attack()
    {
        if (cooldown > 0)
            return;

        attacking = true;
        cooldown = attack.cooldown;
        Instantiate(attack, transform.position, Quaternion.identity);
        Invoke(nameof(ReadyAttack), attack.delay + 0.2f);
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
