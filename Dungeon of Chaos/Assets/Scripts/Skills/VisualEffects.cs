using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    [SerializeField] private float duration;

    void Start()
    {
        Invoke(nameof(End), duration);
    }

    private void End()
    {
        Destroy(gameObject);
    }
}
