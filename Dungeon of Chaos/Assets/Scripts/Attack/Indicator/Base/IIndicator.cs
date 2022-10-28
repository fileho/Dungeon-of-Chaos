using System.Collections;
using UnityEngine;

public abstract class IIndicator : MonoBehaviour
{
    public float Duration { get; private set; }
    protected SpriteRenderer sprite;

    public void Init(IndicatorConfiguration indicatorConfiguration)
    {
        InitSprites();
        ApplyConfigurations(indicatorConfiguration);
    }


    protected virtual void InitSprites() {
        sprite = transform.Find("Primary").GetComponent<SpriteRenderer>();
    }


    protected virtual void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration) {
        Duration = indicatorConfiguration.duration;
        sprite.color = indicatorConfiguration.color;
        sprite.transform.localScale *= indicatorConfiguration.scale; 
    }

    public abstract void Use();
    protected abstract IEnumerator ShowIndicator();
    

    protected virtual void CleanUp()
    {
        Destroy(gameObject);
    }
}
