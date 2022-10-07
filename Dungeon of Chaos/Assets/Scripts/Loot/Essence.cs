using UnityEngine;

public class Essence : MonoBehaviour
{
    public enum EssenceType
    {
        health,
        mana,
        stamina,
        xp        
    }

    [SerializeField] private EssenceType essenceType;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float force = 5f;

    private float value;
  
    void Start()
    {
        Invoke(nameof(CleanUp), lifetime);
        GetComponent<Rigidbody2D>().AddForce(80 * force * Random.insideUnitCircle);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    public float GetChance(Enemy e)
    {
        return essenceType == EssenceType.xp ? 1 : e.lootModifiers.GetEssenceChance(e.stats.GetLevel());
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
            case EssenceType.xp:
                value = e.lootModifiers.GetXPValue(e.stats.GetLevel());
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

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
            case EssenceType.xp:
                Character.instance.stats.GetLevellingData().ModifyCurrentXP(((int)value));
                break;
        }
        CleanUp();
    }
}
