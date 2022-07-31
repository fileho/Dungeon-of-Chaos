using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class IMovement : ScriptableObject
{
    public abstract IMovement Init(Transform transform, Stats stats);
    public abstract void Move();
    public abstract Vector2 GetMoveDir();
}
