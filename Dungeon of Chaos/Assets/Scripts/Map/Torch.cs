using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private Light2D light2d;

    private float state;
    private Vector2 pos2d;
    private const float range = 18;
    private const float maxIntensity = 0.6f;

    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        pos2d = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 charPos = Character.instance.transform.position;
        float dist = (pos2d - charPos).magnitude;

        float offset = dist < range ? 1f : dist < range * 1.5f ? 0f : -1f;
        offset *= Time.deltaTime * 0.5f;

        state = Mathf.Clamp(state + offset, 0, maxIntensity);
        light2d.intensity = state;
        
        if (state < 0.01f && ps.isPlaying)
            ps.Stop();
        if (state > 0.01 && !ps.isPlaying)
            ps.Play();
    }
}
