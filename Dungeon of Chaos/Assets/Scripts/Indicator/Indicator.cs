using System.Collections;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] IndicatorConfiguration indicatorConfiguration;
    private float delay;
    private SpriteRenderer sprite;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        delay = indicatorConfiguration.delay;
        sprite.sprite = indicatorConfiguration.sprite;

        var p = transform.position;
        p.z += 5;
        transform.position = p;
        Use();
    }

    public float GetDelay() {
        return delay;
    }

    private void Use() {
        StartCoroutine(ShowIndicator());
    }


    private IEnumerator ShowIndicator()
    {
        float time = 0f;
        while (time < delay)
        {
            time += Time.deltaTime;
            float t = time / delay;
            t *= 0.5f;
            sprite.color = Color.Lerp(Color.black, Color.white, t);
            yield return null;
        }
        sprite.color = Color.white;
        Invoke(nameof(CleanUp), 0.25f);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
}
