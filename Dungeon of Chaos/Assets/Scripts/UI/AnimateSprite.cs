using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSprite : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color color;

    private float time;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        color = sprite.color;
    }
    void Update()
    {
        time += Time.deltaTime;

        float t = Mathf.Sin(time * 8) * 0.5f + 0.5f;
        sprite.color = Color.Lerp(color, color * 0.9f, t);
    }
}
