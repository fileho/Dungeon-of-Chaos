using UnityEngine;

/// <summary>
/// Creates placed in the dungeon
/// </summary>
public class Box : MonoBehaviour
{
    [SerializeField]
    private SoundSettings crateDestroySFX;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyBox();
    }

    /// <summary>
    /// Destroy the crate
    /// </summary>
    protected virtual void DestroyBox()
    {
        var ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        SoundManager.instance.PlaySound(crateDestroySFX);
        Invoke(nameof(CleanUp), ps.main.duration);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }
}
