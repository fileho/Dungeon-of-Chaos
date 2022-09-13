using System.Collections;
using UnityEngine;

public class ProjectileDeprecated : MonoBehaviour
{
    private float damage;
    public float speed;
    [Tooltip("How fast should the projectile appear")]
    public float delay = 0.5f;
    [Tooltip("Should is go directly towards player?")]
    [Range(0,1)]
    public float offset = 0f;

    [SerializeField] private float homingStrength = 0;

    private Transform caster;

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
        if (homingStrength == 0)
            return;

        rb.drag = 0.5f;
        Vector2 dir = Character.instance.transform.position - transform.position;
        dir.Normalize();
        rb.AddForce(homingStrength * Time.fixedDeltaTime * dir);
    }

    public void SetStats(float damage, float speed, Transform caster)
    {
        this.damage = damage;
        this.speed = speed;
        this.caster = caster;
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
            // move it with the player
            if (caster)
                transform.position = caster.position;

            yield return null;
        }

        collider.enabled = true;

        int enemyLayer = LayerMask.NameToLayer("EnemyAttack");
        Vector2 goalPos = gameObject.layer == enemyLayer
            ? Character.instance.transform.position
            : Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = goalPos - (Vector2)transform.position;
        dir.Normalize();
        dir += offset * Random.insideUnitCircle;
        dir.Normalize();

        rb.AddForce(100 * speed * dir);
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

        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
