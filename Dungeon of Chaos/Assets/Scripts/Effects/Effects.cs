using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Effects/Effects")]
public class Effects : Ieffects
{
    private SpriteRenderer sprite;
    private Color spriteColor;

    private MonoBehaviour monoBehaviour;

    public override Ieffects Init(Transform transform)
    {
        sprite = transform.GetComponent<SpriteRenderer>();
        spriteColor = sprite.color;

        monoBehaviour = transform.GetComponent<MonoBehaviour>();

        return this;
    }

    public override void TakeDamage()
    {
        monoBehaviour.StartCoroutine(FlashRedEffect());
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
