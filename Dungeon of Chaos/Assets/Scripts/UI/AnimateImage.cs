using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateImage : MonoBehaviour
{
    private Image image;

    private float time;

    void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        time += Time.deltaTime;

        float t = Mathf.Sin(time * 0.1f) * 0.5f + 0.5f;
        image.color = Color.Lerp(Color.white, new Color(.9f, 0.9f, 0.9f), t);
    }
}
