using System.Collections.Generic;
using UnityEngine;

public class TemporalVisualEffect : MonoBehaviour
{
    private List<TemporalEffect> effects;
    private List<bool> activeEffects;
    private Material material = null;
    private Unit t;

    public void Init(Unit source, List<TemporalEffect> effects, Unit target)
    {
        this.effects = effects;
        t = target;
        activeEffects = new List<bool>();
        foreach (var e in effects)
        {
            e.Use(source, new List<Unit>() { target });
            activeEffects.Add(true);
        }

        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            material = target.gameObject.GetComponent<SpriteRenderer>().sharedMaterial;
            target.gameObject.GetComponent<SpriteRenderer>().material = gameObject.GetComponent<SpriteRenderer>().sharedMaterial;
        }
    }
    
    void Update()
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].Update())
                activeEffects[i] = false;                
        }

        if (!activeEffects.Contains(true))
        {
            if (t != null && t.gameObject != null && t.gameObject.GetComponent<SpriteRenderer>() != null && material != null)
                t.gameObject.GetComponent<SpriteRenderer>().material = material;
            Destroy(gameObject);
        }
    }
}
