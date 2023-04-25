using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// Torch is lit only when the player is nearby
/// </summary>
public class Torch : MonoBehaviour
{
    private Light2D light2d;

    private float state;
    private Vector2 pos2d;
    private const float range = 18;
    private const float maxIntensity = 0.6f;

    private ParticleSystem ps;

    [SerializeField]
    private SoundSettings torchLoopingSFX;
    private SoundData sfx = null;
    [SerializeField]
    private SoundSettings torchStartBurningSFX;

    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        pos2d = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // There is no player in the dungeon generation level
        if (!Character.instance)
            return;
        Vector2 charPos = Character.instance.transform.position;
        float dist = (pos2d - charPos).magnitude;
        // Ambient sounds
        if (dist <= range)
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

        // Turn up / down the intensity when player gets too close / far
        float offset = dist < range ? 1f : dist < range * 1.5f ? 0f : -1f;
        offset *= Time.deltaTime * 0.5f;

        state = Mathf.Clamp(state + offset, 0, maxIntensity);
        light2d.intensity = state;

        // Control smoke particles
        if (state < 0.01f && ps.isPlaying)
            ps.Stop();
        if (state > 0.01 && !ps.isPlaying)
        {
            ps.Play();
            SoundManager.instance.PlaySound(torchStartBurningSFX);
        }
    }

    private void PlaySound()
    {
        float distance = Vector2.Distance(Character.instance.transform.position, transform.position);
        torchLoopingSFX.SetVolumeFromDistance(distance, range);

        sfx = SoundManager.instance.PlaySoundLooping(torchLoopingSFX);
    }

    private void UpdateSound()
    {
        float distance = Vector2.Distance(Character.instance.transform.position, pos2d);

        float volume = torchLoopingSFX.GetVolumeFromDistance(distance, range);
        SoundManager.instance.UpdateLoopingSound(sfx, volume);
    }
}
