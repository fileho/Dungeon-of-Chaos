using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Pickable essences, serves as loot
/// </summary>
public class Essence : MonoBehaviour
{
    public enum EssenceType
    {
        health,
        mana,
        stamina,
        xp
    }

    [SerializeField]
    private EssenceType essenceType;
    // Lifetime in seconds
    [SerializeField]
    private float lifetime = 5f;
    // How fast should the essence travel on spawn
    [SerializeField]
    private float force = 5f;

    [SerializeField]
    private SoundSettings essencePickupSFX;

    // The value of the essence based on its type and the enemy that spawned it
    private float value;

    void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        // Make the essence move and rotate
        rb.AddForce(80 * force * Random.insideUnitCircle);
        rb.AddTorque((Random.value - 0.5f) * 200);
        StartCoroutine(SmoothCleanup());
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Calculates the chances to spawn essence for a given enemy
    /// </summary>
    /// <param name="e">enemy that was killed</param>
    /// <returns></returns>
    public float GetChance(Enemy e)
    {
        return essenceType == EssenceType.xp ? 1 : e.lootModifiers.GetEssenceChance(e.stats.GetLevel());
    }

    /// <summary>
    /// Sets the value for the essence based on the enemy and his level
    /// </summary>
    /// <param name="e">enemy that was killed</param>
    public void SetValue(Enemy e)
    {
        switch (essenceType)
        {
        case EssenceType.health:
            value = e.lootModifiers.GetHealthEssence(e.stats.GetLevel());
            break;
        case EssenceType.stamina:
            value = e.lootModifiers.GetStaminaEssence(e.stats.GetLevel());
            break;
        case EssenceType.mana:
            value = e.lootModifiers.GetManaEssence(e.stats.GetLevel());
            break;
        case EssenceType.xp:
            value = e.lootModifiers.GetXPValue(e.stats.GetLevel());
            break;
        }
    }

    public void SetValue(float val)
    {
        value = val;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        Collect();
    }

    private void Collect()
    {
        SoundManager.instance.PlaySound(essencePickupSFX);
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
        case EssenceType.xp:
            Character.instance.stats.ModifyCurrentXP((int)(value * Character.instance.stats.GetXPModifier()));
            break;
        default:
            throw new ArgumentOutOfRangeException();
        }
        CleanUp();
    }

    /// <summary>
    /// Smooth fadeout of the essence so it does not disappear in from of the player
    /// </summary>
    private IEnumerator SmoothCleanup()
    {
        yield return new WaitForSeconds(lifetime);

        var sp = GetComponent<SpriteRenderer>();
        const float duration = 5f;
        float time = 0;

        while (time <= duration)
        {
            float t = time / duration;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 1 - t);
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
