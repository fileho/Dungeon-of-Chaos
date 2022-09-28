using UnityEngine;
using System.Collections.Generic;

public class VisualEffects : MonoBehaviour
{
    private List<ISkillEffect> effects;
    private Unit source;

    public void Init(float duration, Unit source, List<ISkillEffect> effects)
    {
        this.source = source;
        this.effects = effects;
        
        Invoke(nameof(End), duration);
    }
    private void End()
    {
        foreach (ISkillEffect e in effects)
            e.Use(source);
        Destroy(gameObject);
    }
}
