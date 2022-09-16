using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Visuals")]
public class VisualsAndSounds : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private SoundSettings sfx = new SoundSettings();

    [SerializeField] private float duration;

    [SerializeField] protected List<ISkillEffect> effects;

    protected override void Apply(Unit unit)
    {
        Instantiate(vfx, unit.transform);
        vfx.GetComponent<VisualEffects>().Init(duration, unit, effects);
        SoundManager.instance.PlaySound(sfx.GetName(), sfx.GetVolume(), sfx.GetPitch());
    }
}
