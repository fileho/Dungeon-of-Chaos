using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/AoEVisuals")]
public class AoEVisual : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private SoundSettings sfx = new SoundSettings();

    [SerializeField] private float duration;

    protected override void Apply(Unit unit)
    {
        Instantiate(vfx, unit.transform);
        vfx.GetComponent<AoEVisualEffect>().Init(range, duration);
        SoundManager.instance.PlaySound(sfx.GetName(), sfx.GetVolume(), sfx.GetPitch());
    }
}
