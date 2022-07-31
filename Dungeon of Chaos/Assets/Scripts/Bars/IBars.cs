using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IBars : ScriptableObject
{
    public abstract IBars Init(Transform transform, Stats stats);
    public abstract void UpdateHpBar();
    public abstract void UpdateManaBar();
    public abstract void UpdateStaminaBar();

    public void UpdateAllBars()
    {
        UpdateHpBar();
        UpdateManaBar();
        UpdateStaminaBar();
    }


}
