using UnityEngine;

public class Essence : MonoBehaviour
{
    public enum EssenceType
    {
        health,
        mana,
        stamina
    }

    [SerializeField] private EssenceType essenceType;
    [SerializeField] private float lifetime = 5f;

    private float value;
  
    void Start()
    {
        Invoke(nameof(CleanUp), lifetime);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    public float GetChance(Enemy e)
    {
        return e.lootModifiers.GetEssenceChance(e.stats.GetLevel());
    }

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
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Player")) return;

        Collect();
    }

    private void Collect()
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
