using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Box : MonoBehaviour
{
    public void BakeShadows()
    {
        var shadow = transform.Find("Shadow").GetComponent<ShadowCaster2D>();
        Debug.Log("Bake");

        BindingFlags accessFlagsPrivate = BindingFlags.NonPublic | BindingFlags.Instance;
        FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);

        Vector3[] points = new[] { new Vector3(0.5f, 0.5f, 0), new Vector3(0.5f, -0.5f, 0),
                                   new Vector3(-0.5f, -0.5f, 0), new Vector3(-0.5f, 0.5f, 0) };
        shapePathField.SetValue(shadow, points);
    }

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
