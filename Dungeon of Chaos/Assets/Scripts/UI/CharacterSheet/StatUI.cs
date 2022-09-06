using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class StatUI : MonoBehaviour
{
    [SerializeField] UnityEvent getStat;
    private float value;

    public void UpdateStat()
    {
        getStat.Invoke();
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }

    public void SetPhysicalDamage()
    {
        value = Character.instance.stats.GetPhysicalDamage();
    }

    public void SetSpellPower()
    {
        value = Character.instance.stats.GetSpellPower();
    }

    public void SetMaxHP()
    {
        value = Character.instance.stats.GetMaxHealth();
    }

    public void SetMaxMana()
    {
        value = Character.instance.stats.GetMaxMana();
    }

    public void SetMaxStamina()
    {
        value = Character.instance.stats.GetMaxStamina();
    }

    public void SetStrength()
    {
        value = Character.instance.stats.GetStrength();
    }

    public void SetIntelligence()
    {
        value = Character.instance.stats.GetIntelligence();
    }

    public void SetConstitution()
    {
        value = Character.instance.stats.GetConstitution();
    }

    public void SetEndurance()
    {
        value = Character.instance.stats.GetEndurance();
    }

    public void SetWisdom()
    {
        value = Character.instance.stats.GetWisdom();
    }
}
