using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private List<ISkillEffect> effects;
    private Unit source;
    private float speed;
    private Vector2 target;

    private new Collider2D collider;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    private Vector3 targetScale;

    [SerializeField] private GameObject impactVFX;
    [SerializeField] private GameObject spellCastVFX;

    [SerializeField]
    private float delay;
    [SerializeField]
    private SoundSettings castSFX;
    [SerializeField]
    private SoundSettings impactSFX;
    [SerializeField]
    private SoundSettings flightSFX;

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        targetScale = transform.localScale;

        StartCoroutine(ExecuteAction());
    }

    public void Init(List<ISkillEffect> effects, Unit source, float speed, Vector3 target)
    {
        this.effects = effects;
        this.source = source;
        this.speed = speed;
        this.target = target;
    }

    private IEnumerator ExecuteAction()
    {
        float time = 0f;
        Vector2 dir = target - (Vector2)transform.position;
        dir.Normalize();

        transform.Rotate(0, 0, Vector2.SignedAngle(Vector2.down, dir));
        SoundManager.instance.PlaySound(castSFX);
        if (spellCastVFX != null)
        {
            var castVFX = Instantiate(spellCastVFX, source.gameObject.transform);
            Destroy(castVFX, delay);
        }
        while (time < delay)
        {            
            time += Time.deltaTime;
            float t = time / delay;
            transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, t);

            if (source)
                transform.position = source.transform.position;

            yield return null;
        }

        collider.enabled = true;

        rb.AddForce(100 * speed * dir);
        SoundManager.instance.PlaySound(flightSFX);
        Invoke(nameof(CleanUp), 10f);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore triggers
        if (collision.isTrigger)
            return;

        // Don't destroy projectile with essences
        if (collision.transform.GetComponent<Essence>())
            return;

        SoundManager.instance.PlaySound(impactSFX);
        GameObject impactPs = Instantiate(impactVFX, transform.position, Quaternion.identity);
        Destroy(impactPs, 1f);
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
