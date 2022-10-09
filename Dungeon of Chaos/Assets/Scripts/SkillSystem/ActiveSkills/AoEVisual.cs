using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/AoEVisuals")]
public class AoEVisual : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private SoundSettings sfx = new SoundSettings();

    [SerializeField] private float duration;
    [SerializeField] protected List<ISkillEffect> effects;

    protected override void Apply(Unit unit)
    {
        var clone = Instantiate(vfx, unit.transform);
        clone.GetComponent<AoEVisualEffect>().Init(range, duration, unit, effects);
        //SoundManager.instance.PlaySound(sfx.GetName(), sfx.GetVolume(), sfx.GetPitch());
    }
}
