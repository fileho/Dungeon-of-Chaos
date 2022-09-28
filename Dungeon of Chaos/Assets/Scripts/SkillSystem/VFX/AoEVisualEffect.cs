using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEVisualEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem aoeEffect;

    private float range;
    private float duration;

    public void Init(float range, float duration)
    {
        this.range = range;
        this.duration = duration;
    }

    private void Start()
    {
        ParticleSystem.MainModule aoeMain = aoeEffect.main;
        aoeMain.startSize = range;
        aoeMain.duration = duration;
        aoeEffect.Play();

        Invoke(nameof(CleanUp), duration);      
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
}
