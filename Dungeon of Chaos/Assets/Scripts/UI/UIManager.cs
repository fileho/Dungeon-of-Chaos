using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider manaBar;
    [SerializeField] private Slider staminaBar;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void SetHealthBar(float value)
    {
        healthBar.value = value;
    }    
    public void SetManaBar(float value)
    {
        manaBar.value = value;
    }

    public void SetStaminaBar(float value)
    {
        staminaBar.value = value;
    }
}
