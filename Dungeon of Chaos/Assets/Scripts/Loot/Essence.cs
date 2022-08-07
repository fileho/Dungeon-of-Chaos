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
    [SerializeField] private float value = 10f;
    [SerializeField] private float lifetime = 5f;


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
