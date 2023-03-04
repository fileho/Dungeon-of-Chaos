using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shiny pickable crystals
/// </summary>
public class Crystal : MonoBehaviour
{
    [SerializeField]
    private float hpBoost;
    [SerializeField]
    private float manaBoost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character.instance.stats.RegenerateHealth(hpBoost);
            Character.instance.stats.RegenerateMana(manaBoost);
            Destroy(gameObject);
        }
    }
}
