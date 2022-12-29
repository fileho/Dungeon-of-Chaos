using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField]
    private VolumeProfile vp;

    private ColorCurves curve;

    private void Awake()
    {
        vp.TryGet(out curve);
        CleanUp();
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    public void Change(float strength)
    {
        curve.red.value.MoveKey(0, new Keyframe(0, strength * 0.075f));
        curve.red.value.SmoothTangents(0, 1);
    }

    private void CleanUp()
    {
        curve.red.value.MoveKey(0, new Keyframe(0, 0));
        curve.red.value.SmoothTangents(0, 1);
        if (curve.red.value.length < 3)
            curve.red.value.AddKey(0.1f, 0.1f);
    }
}
