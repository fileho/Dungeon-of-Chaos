using System.Collections.Generic;
using UnityEngine;

public class TemporalVisualEffect : MonoBehaviour
{
    [SerializeField] private bool applyOnWeapon = true;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material effectMaterial;

    private List<TemporalEffect> effects;
    private List<bool> activeEffects;
    private SpriteRenderer t;

    public void Init(Unit source, List<TemporalEffect> effects, Unit target)
    {
        this.effects = effects;
        t = applyOnWeapon
            ? target.GetComponentInChildren<Weapon>().gameObject.GetComponentInChildren<SpriteRenderer>(false)
            : target.gameObject.GetComponent<SpriteRenderer>();
        
        activeEffects = new List<bool>();
        foreach (var e in effects)
        {
            e.Use(source, new List<Unit>() { target });
            activeEffects.Add(true);
        }

        if (effectMaterial != null && t != null && defaultMaterial != null)
        {
            t.material = effectMaterial;
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
            if (t != null)
            {
                t.material = defaultMaterial;
            }
            Destroy(gameObject);
        }
    }
}
