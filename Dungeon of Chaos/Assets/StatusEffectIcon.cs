using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatusEffectType
{
    AttackBoost,
    SpellPowerBoost,
    Regeneration,
    Burn,
    FlamingSword
}
public class StatusEffectIcon : MonoBehaviour
{
    [SerializeField] private Image highlight;
    [SerializeField] private StatusEffectType effectType;

    public void UpdateTime(float duration, float timeLeft)
    {
        highlight.fillAmount = timeLeft / duration;
    }

    public void Show()
    {
        GetComponent<CanvasGroup>().alpha = 1;
    }

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }

    public StatusEffectType GetEffectType()
    {
        return effectType;
    }
}
