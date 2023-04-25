using UnityEngine;

/// <summary>
/// Interface of defining movements
/// </summary>
public abstract class IMovement : ScriptableObject
{
    public abstract IMovement Init(Transform transform, Stats stats);
    public abstract void Move();
    public abstract void MuteSfx();
    /// <summary>
    /// Direction of the last movement
    /// </summary>
    public abstract Vector2 GetMoveDir();
}
