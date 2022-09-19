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
        var clone = Instantiate(vfx, unit.transform);
        clone.GetComponent<VisualEffects>().Init(duration, unit, effects);
        SoundManager.instance.PlaySound(sfx);
    }
}
