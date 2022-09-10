using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsAndSounds : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private AudioClip sfx;

    [SerializeField] private float duration;
    [SerializeField] private float wait;

    public override void Use(Unit unit)
    {
        Instantiate(vfx, unit.transform);
        vfx.GetComponent<VisualEffects>().Init(duration);
        //TODO: Play Sound
    }
}
