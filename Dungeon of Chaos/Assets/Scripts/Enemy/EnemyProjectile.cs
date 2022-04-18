using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyAttack
{
    private new Collider2D collider;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Use();
    }

    private void Use()
    {
        StartCoroutine(ExecuteAttack());
    }

    private IEnumerator ExecuteAttack()
    {
        float time = 0;

        while (time < delay)
        {
            time += Time.deltaTime;
            float t = time / delay;
            sprite.color = Color.Lerp(Color.yellow, new Color(1f, 0.5f, 0f), t);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            yield return null;
        }

        collider.enabled = true;

        Vector2 dir = Character.instance.transform.position - transform.position;
        dir = dir.normalized;
        rb.AddForce(200 * dir);
        Invoke(nameof(CleanUp), 10f);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " takes damage " + damage);
    }
}
