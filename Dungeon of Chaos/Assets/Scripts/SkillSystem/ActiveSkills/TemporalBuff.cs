using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "SO/Skills/TemporalBuff")]
public class TemporalBuff : ISkillEffect
{
    [SerializeField] private float duration;
    [SerializeField] private float value;

    public float timeLeft;
    private float buff;

    [SerializeField] private UnityEvent<float> changeStat;

    public override void Use(Unit unit)
    {
        //TODO: Add to effects on the character
        buff = value * unit.stats.GetSpellPower();
        timeLeft = duration;
        changeStat.Invoke(buff);
    }

    public void UpdateTime()
    {
        if (timeLeft > 0)
            timeLeft -= Time.deltaTime;
        else
        {
            changeStat.Invoke(-buff);
            //TODO: Delete effect from character
        }
    }

}
