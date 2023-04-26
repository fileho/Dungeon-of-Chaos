using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Visual effect for AoE skills
/// </summary>
public class AoEVisualEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem aoeEffect;

    private float range;
    private float duration;
    private Unit source;
    private List<ISkillEffect> effects;

    public void Init(float range, float duration, Unit source, List<ISkillEffect> effects)
    {
        this.range = range;
        this.duration = duration;
        this.source = source;
        this.effects = effects;
    }

    private void Start()
    {
        ParticleSystem.MainModule aoeMain = aoeEffect.main;
        aoeMain.startSize = range;
        aoeMain.duration = duration;
        Invoke(nameof(CleanUp), duration);
        aoeEffect.Play();

             
    }

    private void CleanUp()
    {
        foreach (ISkillEffect e in effects)
            e.Use(source);
        Destroy(gameObject);
    }
}
