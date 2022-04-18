using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EnemyMelee : EnemyAttack
{
    private new Collider2D collider;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        collider.enabled = false;

        Rotate();
        Use();
    }

    private void Rotate()
    {
        Vector2 dir = Character.instance.transform.position - transform.position;

        transform.up = dir;
    }

    private void Use()
    {
        StartCoroutine(ExecuteAttack());
    }

    private IEnumerator ExecuteAttack()
    {
        float time = 0f;

        while (time < delay)
        {
            time += Time.deltaTime;
            float t = time / delay;
            t *= 0.5f;
            sprite.color = Color.Lerp(Color.black, Color.white, t);
            yield return null;
        }

        collider.enabled = true;
        sprite.color = Color.white;
        Invoke(nameof(CleanUp), 0.25f);
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
