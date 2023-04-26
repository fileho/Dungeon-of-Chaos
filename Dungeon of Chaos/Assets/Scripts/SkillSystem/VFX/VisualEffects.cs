 using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Generic one shot VFX
/// </summary>
public class VisualEffects : MonoBehaviour
{
    private List<ISkillEffect> effects;
    protected Unit source;

    public virtual void Init(float duration, Unit source, List<ISkillEffect> effects)
    {
        this.source = source;
        this.effects = effects;
        
        Invoke(nameof(CastEffects), duration);
    }
    protected void CastEffects()
    {
        foreach (ISkillEffect e in effects)
            e.Use(source);
        Destroy(gameObject, 1.5f);
    }
}
