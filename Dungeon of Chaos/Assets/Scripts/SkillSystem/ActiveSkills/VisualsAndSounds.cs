using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsAndSounds : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private AudioClip sfx;

    [SerializeField] private float duration;

    [SerializeField] protected List<ISkillEffect> effects;

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        return;
    }

    protected override void Apply(Unit unit)
    {
        Instantiate(vfx, unit.transform);
        vfx.GetComponent<VisualEffects>().Init(duration, unit, effects);
        //TODO: Play Sound
    }
}
