using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LavaPit : MonoBehaviour
{
    [SerializeField]
    private float dps = 30;

    [Header("SFX")]
    [SerializeField] SoundSettings burnSFX;
    private SoundData sfx = null;

    public void StartLights()
    {
        StartCoroutine(InterpolateLights());
    }

    private void OnTriggerStay2D(Collider2D collider2d)
    {
        if (!collider2d.CompareTag("Player"))
            return;

        collider2d.GetComponent<Unit>().TakeDamage(dps * Time.deltaTime, false);
        sfx = SoundManager.instance.PlaySoundLooping(burnSFX);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        SoundManager.instance.StopLoopingSound(sfx);
    }

    private IEnumerator InterpolateLights()
    {
        const float duration = 3f;
        var light2d = GetComponent<Light2D>();

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            light2d.intensity = Mathf.Clamp01(time / duration);
            yield return null;
        }
    }
}
