using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Stats stats;

    protected SpriteRenderer sprite; 
    protected Rigidbody2D rb;
    protected Weapon weapon;

    private Color spriteColor;

    protected void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponentInChildren<Weapon>();

        spriteColor = sprite.color;

        Init();
    }

    protected virtual void Init() { }

    public void TakeDamage(float value)
    {
        stats.ConsumeHealth(value);
        StartCoroutine(FlashRedEffect());
        TakeDamageSideEffect();
        if (stats.IsDead())
            Die();
    }

    protected virtual void TakeDamageSideEffect() { }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected IEnumerator FlashRedEffect()
    {
        const float duration = 0.35f;

        sprite.color = Color.red;

        float t = 0;
        while (t < duration)
        {
            sprite.color = Color.Lerp(spriteColor, Color.red, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        sprite.color = spriteColor;
    }
}
