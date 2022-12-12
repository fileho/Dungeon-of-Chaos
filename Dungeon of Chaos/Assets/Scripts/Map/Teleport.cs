using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    private GameObject tooltipCanvas;
    private float range = 2f;

    [Header("SFX")]
    [SerializeField] private SoundSettings teleportLooping;
    [SerializeField] private SoundSettings teleportUse;
    private float maxDistance = 15f;
    private SoundData sfx = null;

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
        if (Vector2.Distance(Character.instance.transform.position, transform.position) <= maxDistance)
        {
            if (sfx == null)
                PlaySound();
            else
                UpdateSound();
        }
        else
        {
            SoundManager.instance.StopLoopingSound(sfx);
            sfx = null;
        }
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
        SoundManager.instance.PlaySound(teleportUse);
        FindObjectOfType<GameController>().LevelComplete();
    }

    private void PlaySound()
    {
        float distance = Vector2.Distance(Character.instance.transform.position, transform.position);
        teleportLooping.SetVolumeFromDistance(distance, maxDistance);

        sfx = SoundManager.instance.PlaySoundLooping(teleportLooping);
    }

    private void UpdateSound()
    {
        float distance = Vector2.Distance(Character.instance.transform.position, transform.position);

        float volume = teleportLooping.GetVolumeFromDistance(distance, maxDistance);
        SoundManager.instance.UpdateLoopingSound(sfx, volume);
    }
}
