using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TemporalEffect : ISkillEffect
{
    [SerializeField] protected float duration;
    [SerializeField] protected float value;
    
    protected StatusEffectIcon effectIcon;

    protected float timeLeft;
    protected float val;

    protected List<Unit> targets;

    public override string[] GetEffectsValues(Unit owner)
    {
        return new string[] { Math.Round(GetValue(owner),2).ToString(), value.ToString(), duration.ToString() + " seconds" };
    }

    protected float GetValue(Unit owner) 
    {
        return value * owner.stats.GetSpellPower();
    }

    protected abstract void Init();

    protected void InitStatusIcon(StatusEffectType type)
    {
        var icons = FindObjectsOfType<StatusEffectIcon>();
        foreach (StatusEffectIcon icon in icons)
        {
            if (icon.GetEffectType() == type)
            {
                effectIcon = icon;
                return;
            }
        }
    }

    protected override void ApplyOnTargets(Unit unit, List<Unit> targets)
    {
        this.targets = targets;
        val = GetValue(unit);
        timeLeft = duration;
        ApplyEffect();
        Init();
        if (targets.Contains(Character.instance) && effectIcon != null)
        {
            effectIcon.Show();
            effectIcon.UpdateTime(duration, timeLeft);
        }
    }

    protected abstract void ApplyEffect();

    public abstract bool Update();

    protected bool UpdateTime()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            if (effectIcon != null)
                effectIcon.UpdateTime(duration, timeLeft);
            return true;
        }
        if (effectIcon != null)
            effectIcon.Hide();
        return false;
    }
}
