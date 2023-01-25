using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/AoEVisuals")]
public class AoEVisual : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private SoundSettings sfx = new SoundSettings();

    [SerializeField] private float duration;
    [SerializeField] protected List<ISkillEffect> effects;

    public override string[] GetEffectsValues(Unit owner)
    {
        List<string> descriptionValues = new List<string>();
        foreach (ISkillEffect effect in effects)
        {
            var d = effect.GetEffectsValues(owner);
            if (d == null)
                continue;
            for (int i = 0; i < d.Length; i++)
            {
                if (d[i] != null)
                    descriptionValues.Add(d[i]);
            }
        }

        return descriptionValues.ToArray();
    }

    protected override void Apply(Unit unit)
    {
        var clone = Instantiate(vfx, unit.transform);
        clone.GetComponent<AoEVisualEffect>().Init(range, duration, unit, effects);
    }
}
