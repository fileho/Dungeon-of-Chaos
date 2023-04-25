using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shiny pickable crystals, restores hp and mana
/// Similar to essences
/// </summary>
public class Crystal : MonoBehaviour
{
    [SerializeField]
    private float hpBoost;
    [SerializeField]
    private float manaBoost;
    [SerializeField]
    private SoundSettings crystalPickupSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(crystalPickupSFX);
            Character.instance.stats.RegenerateHealth(hpBoost);
            Character.instance.stats.RegenerateMana(manaBoost);
            Destroy(gameObject);
        }
    }
}
