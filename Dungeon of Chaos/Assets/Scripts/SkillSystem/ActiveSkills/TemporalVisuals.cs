using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporalVisuals : ISkillEffect
{
    [SerializeField] private GameObject vfx;
    [SerializeField] private List<TemporalEffect> effects;
    
    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        foreach (Unit t in targets)
        {
            Instantiate(vfx, t.transform);
            vfx.GetComponent<TemporalVisualEffect>().Init(unit, effects, t);
        }
    }

}
