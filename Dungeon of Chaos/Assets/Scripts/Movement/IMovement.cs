using UnityEngine;


public abstract class IMovement : ScriptableObject
{
    public abstract IMovement Init(Transform transform, Stats stats);
    public abstract void Move(SoundSettings footstepsSFX);
    public abstract Vector2 GetMoveDir();
}
