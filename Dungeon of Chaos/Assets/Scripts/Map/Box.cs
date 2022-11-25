using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyBox();
    }

    private void DestroyBox()
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
