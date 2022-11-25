using UnityEngine;

public class Box : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyBox();
    }

    protected virtual void DestroyBox()
    {
        var ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        Invoke(nameof(CleanUp), ps.main.duration);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
}
