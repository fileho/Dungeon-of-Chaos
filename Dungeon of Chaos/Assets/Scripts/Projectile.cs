using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    public float speed = 200f;
    [Tooltip("How fast should the projectile appear")]
    public float delay = 0.5f;
    [Tooltip("Should is go directly towards player?")]
    [Range(0,1)]
    public float offset = 0f;

    [SerializeField] private float homingStrength = 0;



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

    void FixedUpdate()
    {
        if (homingStrength == 0 && rb.velocity.magnitude < 0.01f)
            return;

        rb.drag = 0.5f;
        Vector2 dir = Character.instance.transform.position - transform.position;
        dir.Normalize();
        rb.AddForce(homingStrength * Time.fixedDeltaTime * dir);
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
        dir.Normalize();
        dir += offset * Random.insideUnitCircle;
        dir.Normalize();

        rb.AddForce(speed * dir);
        Invoke(nameof(CleanUp), 10f);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Character.instance.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
