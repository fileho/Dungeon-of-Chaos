using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/EnemyEffects")]
public class EnemyEffects : Ieffects
{
    private Transform transform;
    private SpriteRenderer sprite;
    private Color spriteColor;
    private Rigidbody2D rb;

    private MonoBehaviour monoBehaviour;

    public override Ieffects Init(Transform transform)
    {
        this.transform = transform;
        sprite = transform.GetComponent<SpriteRenderer>();
        spriteColor = sprite.color;
        rb = transform.GetComponent<Rigidbody2D>();
        monoBehaviour = transform.GetComponent<MonoBehaviour>();

        return this;
    }

    public override void TakeDamage()
    {
        monoBehaviour.StartCoroutine(FlashRedEffect());

        Vector2 dir = (transform.position - Character.instance.transform.position).normalized;
        rb.AddForce(dir * 500);
        Vector2 n = new Vector2(dir.y, -dir.x).normalized;

        monoBehaviour.StartCoroutine(Wiggle(n));
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

    private IEnumerator Wiggle(Vector2 dir)
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