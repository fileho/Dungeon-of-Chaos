using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/Poison")]
public class Poison : RepeatedTemporalEffect
{
    private List<float> hp;

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        this.targets = targets;
        val = GetValue(unit);
        hp = new List<float>();
        foreach (Unit target in this.targets)
        {
            hp.Add(target.stats.GetCurrentHealth());
        }
        ApplyEffect();
    }
    public override bool Update()
    {
        CheckCondition();
        if (targets.Count == 0)
            return false;

        if (ShouldApplyEffect())
        {
            ApplyEffect();
            time -= frequency;
        }
        return true;
    }
        
    protected override void ApplyEffect()
    {
        foreach (Unit target in targets)
            target.TakeDamage(val);
    }

    private void CheckCondition()
    {
        List<int> delete = new List<int>();
        for (int i = 0; i < targets.Count; i++)
        {
            float currentHealth = targets[i].stats.GetCurrentHealth();
            if (targets[i] == null || currentHealth > hp[i])
                delete.Add(i);
            else
                hp[i] = currentHealth;                
        }
        RemoveTargets(delete);
    }

    private void RemoveTargets(List<int> delete)
    {
        int deleted = 0;
        foreach (int index in delete)
        {
            hp.RemoveAt(index - deleted);
            targets.RemoveAt(index - deleted);
            deleted++;
        }
    }
}
