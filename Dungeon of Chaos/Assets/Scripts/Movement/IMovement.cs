using UnityEngine;

public abstract class IMovement : ScriptableObject
{
    public abstract IMovement Init(Transform transform, Stats stats);
    public abstract void Move();
    public abstract void MuteSfx();
    public abstract Vector2 GetMoveDir();
}
