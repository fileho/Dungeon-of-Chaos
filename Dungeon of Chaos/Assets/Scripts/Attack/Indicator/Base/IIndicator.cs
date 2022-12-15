using System.Collections;
using UnityEngine;

public abstract class IIndicator : MonoBehaviour
{
    public float Duration { get; protected set; }
    protected SpriteRenderer sprite;

    public void Init(IndicatorConfiguration indicatorConfiguration)
    {
        InitSprites();
        ApplyConfigurations(indicatorConfiguration);
    }


    protected virtual void InitSprites()
    {
        sprite = transform.Find("Primary").GetComponent<SpriteRenderer>();
    }


    protected virtual void ApplyConfigurations(IndicatorConfiguration indicatorConfiguration)
    {
        Duration = indicatorConfiguration.duration;
    }

    public virtual void Use()
    {
        //SoundManager.instance.PlaySound(indicatorSFX);
        StartCoroutine(ShowIndicator());
    }

    protected abstract IEnumerator ShowIndicator();


    protected virtual void CleanUp()
    {
        Destroy(gameObject);
    }
}
