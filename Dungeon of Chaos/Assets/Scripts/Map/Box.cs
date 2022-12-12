using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private SoundSettings crateDestroySFX;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyBox();
    }

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
