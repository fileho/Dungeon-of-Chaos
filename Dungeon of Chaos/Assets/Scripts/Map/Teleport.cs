using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    private GameObject tooltipCanvas;
    private float range = 2f;

    private void Start()
    {
        tooltipCanvas = transform.GetChild(0).gameObject;
        tooltipCanvas.SetActive(false);
        var t = tooltipCanvas.transform;
        t.rotation = Quaternion.identity;
        t.Translate(0, 3, 0);
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        if (((Vector2)transform.position - (Vector2)Character.instance.transform.position).magnitude < range)
            Travel();
    }


    private void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(false);
    }

    private void Travel()
    {
        FindObjectOfType<GameController>().LevelComplete();
    }
}
