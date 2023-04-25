using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

/// <summary>
/// Indicator of low health
/// Progressively turns the screen more red for low hp
/// Shifts the black color to red as a postprocessing
/// </summary>
public class HealthIndicator : MonoBehaviour
{
    /// <summary>
    /// VolumeProfile that controls the changes in color
    /// </summary>
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

    /// <summary>
    /// Updates the strength of red based on hp
    /// </summary>
    /// <param name="strength">Normalized to [0, 1], 1 means the strongest effect</param>
    public void Change(float strength)
    {
        curve.red.value.MoveKey(0, new Keyframe(0, strength * 0.05f));
        curve.red.value.SmoothTangents(0, 1);
    }

    private void CleanUp()
    {
        curve.red.value.MoveKey(0, new Keyframe(0, 0));
        curve.red.value.SmoothTangents(0, 1);
        if (curve.red.value.length < 3)
            curve.red.value.AddKey(0.02f, 0.02f);
    }
}
