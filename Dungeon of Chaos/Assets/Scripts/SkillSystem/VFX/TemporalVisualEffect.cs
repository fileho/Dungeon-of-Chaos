using System.Collections.Generic;
using UnityEngine;

public class TemporalVisualEffect : MonoBehaviour
{
    private List<TemporalEffect> effects;
    private List<bool> activeEffects;

    public void Init(Unit source, List<TemporalEffect> effects, Unit target)
    {
        this.effects = effects;
        activeEffects = new List<bool>();
        foreach (var e in effects)
        {
            e.Use(source, new List<Unit>() { target });
            activeEffects.Add(true);
        }
    }
    
    void Update()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].DestroyEffect())
                activeEffects[i] = false;                
        }

        if (!activeEffects.Contains(true))
            Destroy(gameObject);
    }
}
