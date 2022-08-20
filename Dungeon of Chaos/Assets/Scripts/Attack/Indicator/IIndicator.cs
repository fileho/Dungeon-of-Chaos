using System.Collections;
using UnityEngine;

public abstract class IIndicator : MonoBehaviour
{
    [SerializeField] protected IndicatorConfiguration indicatorConfiguration;
    public float Duration { get; private set; }
    protected SpriteRenderer sprite;

    void Awake()
    {
        ApplyConfigurations();
        var p = transform.position;
        p.z += 5;
        transform.position = p;
        Use();
    }


    protected virtual void ApplyConfigurations() {
        sprite = GetComponent<SpriteRenderer>();
        Duration = indicatorConfiguration.duration;
        sprite.sprite = indicatorConfiguration.sprite;
        sprite.color = indicatorConfiguration.color;
    }

    public abstract void Use(); 
    protected virtual IEnumerator ShowIndicator()
    {
        float time = 0f;
        while (time < Duration)
        {
            time += Time.deltaTime;
            float t = time / Duration;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, t);
            yield return null;
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        CleanUp();
    }

    protected virtual void CleanUp()
    {
        Destroy(gameObject);
    }
}
