using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/TemporalEffectVisuals")]
public class TemporalVisuals : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private List<TemporalEffect> effects;

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

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        foreach (Unit t in targets)
        {
            var clone = Instantiate(vfx, t.transform);
            clone.GetComponent<TemporalVisualEffect>().Init(unit, effects, t);
        }
    }

}
