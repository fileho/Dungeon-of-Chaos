using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    [SerializeField]
    private SoundSettings buttonClick;
    [SerializeField]
    private SoundSettings buttonHover;

    public void OnHoverSound()
    {
        if (SoundManager.instance)
            SoundManager.instance.PlaySound(buttonHover);
    }

    public void OnClickSound()
    {
        if (SoundManager.instance)
            SoundManager.instance.PlaySound(buttonClick);
    }
}
