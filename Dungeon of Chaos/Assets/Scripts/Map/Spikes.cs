using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private float damage;
    [SerializeField]
    private float delay;

    [SerializeField]
    private Sprite spikesDown;
    [SerializeField]
    private Sprite spikesUp;
    [SerializeField]
    private float reloadTime;

    [Header("SFX")]
    [SerializeField] private SoundSettings spikesUpSFX;
    [SerializeField] private SoundSettings spikesDownSFX;

    private bool attacking = false;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (attacking || !collider.CompareTag("Player"))
            return;

        StartCoroutine(ActivateSpikes());
    }

    IEnumerator ActivateSpikes()
    {
        attacking = true;
        SoundManager.instance.PlaySound(spikesUpSFX);
        yield return new WaitForSeconds(delay);

        spriteRenderer.sprite = spikesUp;
        var results = new List<Collider2D>();
        boxCollider.OverlapCollider(new ContactFilter2D(), results);
        foreach (var r in results)
        {
            var unit = r.gameObject.GetComponent<Unit>();
            if (unit != null)
                unit.TakeDamage(damage);
        }

        yield return new WaitForSeconds(delay);
        SoundManager.instance.PlaySound(spikesDownSFX);
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = spikesDown;

        yield return new WaitForSeconds(reloadTime);
        attacking = false;
    }
}
