using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/SkillEffects/RepeatedEffect")]
public class RepeatedEffect : ISkillEffect
{
    [SerializeField] private float value;
    [SerializeField] private float duration;
    [SerializeField] private float frequency;
    [SerializeField] private UnityEvent<float> changeStat;

    private float timeLeft;
    private float val;
    public override void Use(Unit unit)
    {
        //TODO: Apply on target unit
        val = value * unit.stats.GetSpellPower();
    }

    public void UpdateTime()
    {
        if (timeLeft > 0)
            timeLeft -= Time.deltaTime;
        //TODO: Delete effect from character
    }

    public void ApplyEffect()
    {
        if((duration - timeLeft) % frequency == 0f)
            changeStat.Invoke(val);
    }
}
