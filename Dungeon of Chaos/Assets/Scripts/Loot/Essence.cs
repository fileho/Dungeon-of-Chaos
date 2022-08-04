using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Essence : MonoBehaviour
{
    public enum EssenceType
    {
        health,
        mana,
        stamina
    }

    [SerializeField] private EssenceType essenceType;
    [SerializeField] private float value = 10f;
    [SerializeField] private float lifetime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(CleanUp), lifetime);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        Drop();
    }

    private void Drop()
    {
        switch (essenceType)
        {
            case EssenceType.health:
                Character.instance.stats.RegenerateHealth(value);
                break;
            case EssenceType.mana:
                Character.instance.stats.RegenerateMana(value);
                break;
            case EssenceType.stamina:
                Character.instance.stats.RegenerateStamina(value);
                break;
        }
        CleanUp();
    }
}
