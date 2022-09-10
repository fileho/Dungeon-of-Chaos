using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    public void Init(float duration)
    {
        Invoke(nameof(End), duration);
    }
    private void End()
    {
        Destroy(gameObject);
    }
}
