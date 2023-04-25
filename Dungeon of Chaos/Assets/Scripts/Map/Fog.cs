using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// For travelsable in one direction before the boss room, blocks light, can be rotated to all 4 cardinal directions
/// </summary>
public class Fog : MonoBehaviour
{
    private GameObject tooltipCanvas;
    private const float range = 2f;
    private const float enemyRadius = 10f;

    private void Start()
    {
        tooltipCanvas = transform.GetChild(0).gameObject;
        tooltipCanvas.SetActive(false);
        var t = tooltipCanvas.transform;
        t.rotation = Quaternion.identity;
        t.Translate(0, 3, 0);
    }

    /// <summary>
    /// Bakes shadows for a pixel perfect alignment to avoid shimmering
    /// </summary>
    public void BakeShadows()
    {
        var shadow = transform.parent.Find("Shadow").GetComponent<ShadowCaster2D>();

        BindingFlags accessFlagsPrivate = BindingFlags.NonPublic | BindingFlags.Instance;
        FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);

        Vector3[] points =
            new[] { new Vector3(3, 1, 0), new Vector3(3, -1, 0), new Vector3(-3, -1, 0), new Vector3(-3, 1, 0) };
        shapePathField.SetValue(shadow, points);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        if (!(Remap((Vector2)transform.position - (Vector2)Character.instance.transform.position) < range))
            return;
        if (!CheckEnemyNearby())
        {
            Travel();
        }
        else
        {
            TooltipSystem.instance.ShowMessage("Enemies are nearby!", 1.5f);
        }
    }

    private bool CheckEnemyNearby()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, enemyRadius, LayerMask.GetMask("Enemy"));
        return cols.Any(col => col.GetComponent<Enemy>() != null);
    }

    private float Remap(Vector2 v)
    {
        return Mathf.Abs(transform.up.y - 0) > 0.01f ? new Vector2(v.x / 3, v.y).magnitude
                                                     : new Vector2(v.x, v.y / 3).magnitude;
    }

    // Show tooltip
    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(true);
    }

    // Hide tooltip
    private void OnTriggerExit2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(false);
    }

    // Traverse the fog to the other side
    private void Travel()
    {
        Vector3 dir = new Vector3(0, -6, 0);
        dir = transform.rotation * dir;

        FindObjectOfType<GameController>().StartBossFight();
        StartCoroutine(MovePlayer(dir));
    }

    // Smooth move of the player through the fog
    private IEnumerator MovePlayer(Vector3 dir)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        const float duration = 0.5f;

        Transform trans = Character.instance.transform;
        Vector3 startPos = trans.position;
        float time = 0;

        while (time <= duration)
        {
            float t = time / duration;
            Character.instance.transform.position = Vector3.Lerp(startPos, startPos + dir, t);
            time += Time.deltaTime;
            yield return null;
        }

        GetComponent<BoxCollider2D>().enabled = true;
    }
}
