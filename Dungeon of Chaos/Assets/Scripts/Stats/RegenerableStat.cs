using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerableStat
{
    public float maxValue;
    private float currentValue;

    public void Consume(float value)
    {
        currentValue -= value;
        currentValue = Mathf.Max(currentValue, 0);
    }

    public void Regenerate(float value)
    {
        currentValue += value;
        currentValue = Mathf.Min(currentValue, maxValue);
    }

    public float Ratio()
    {
        return currentValue / maxValue;
    }

    public void Reset()
    {
        currentValue = maxValue;
    }

    public bool HasEnough(float value)
    {
        return currentValue >= value;
    }

    public bool IsDepleted()
    {
        return currentValue <= 0;
    }
}
