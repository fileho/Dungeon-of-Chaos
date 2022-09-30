using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Checkpoint : MonoBehaviour
{
    private GameObject canvas;
    private CharacterSheet characterSheet;

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>().gameObject;
        characterSheet = FindObjectOfType<CharacterSheet>();

        canvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        canvas.SetActive(true);

        if (Input.GetKeyDown(KeyCode.F))
            Interact();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // TODO implement aggro enemies


        if (Input.GetKeyDown(KeyCode.F))
            Interact();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        canvas.SetActive(false);
    }

    private void Interact()
    {
        canvas.SetActive(false);
        characterSheet.Open();
        Rest();
    }

    private void Rest()
    {
        Debug.Log("Location saved");
        SaveSystem.instance.save.SavePosition(transform.position);

        Character.instance.stats.ResetStats();
    }
}
