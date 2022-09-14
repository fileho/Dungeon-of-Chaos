using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VisualsAndSounds : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private SoundSettings sfx;

    [SerializeField] private float duration;

    [SerializeField] protected List<ISkillEffect> effects;

    protected override void Apply(Unit unit)
    {
        Instantiate(vfx, unit.transform);
        vfx.GetComponent<VisualEffects>().Init(duration, unit, effects);
        SoundManager.instance.PlaySound(sfx.name, sfx.volume, sfx.pitch);
    }
}
