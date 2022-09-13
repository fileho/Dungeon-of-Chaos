using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    private List<ISkillEffect> effects;
    private Unit source;
    private float speed;
    private Vector2 target;
    

    private new Collider2D collider;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    [SerializeField] private float delay;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(List<ISkillEffect> effects, Unit source, float speed, Vector2 target)
    {
        this.effects = effects;
        this.source = source;
        this.speed = speed;
        this.target = target;
    }

    private void Use()
    { 
    }

    private IEnumerator ExecuteAction()
    {
        float time = 0f;

        while (time < delay)
        {
            time += Time.deltaTime;
            float t = time / delay;
            sprite.color = Color.Lerp(Color.yellow, new Color(1f, 0.5f, 0f), t);
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);

            if (source)
                transform.position = source.transform.position;

            yield return null;
        }

        collider.enabled = true;
        Vector2 dir = target - (Vector2)transform.position;
        dir.Normalize();

        rb.AddForce(100 * speed * dir);
        Invoke(nameof(CleanUp), 10f);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var e in effects)
            {
                e.Use(source, new List<Unit>() { Character.instance });
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            foreach (var e in effects)
            {
                e.Use(source, new List<Unit>() { collision.GetComponent<Enemy>() });
            }
        }

        Destroy(gameObject);
    }
}
