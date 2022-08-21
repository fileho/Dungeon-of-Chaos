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
        return 1f;
    }

    public void SetValue(Enemy e)
    { 
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
