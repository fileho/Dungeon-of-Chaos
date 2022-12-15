
using UnityEngine;

public abstract class ProjectileConfiguration : ScriptableObject
{
    public float speed = 200f;
    public float delay = 0.5f;
    public float offset = 0f;
    public float destroyTime = 5f;
    public Vector2 scale = Vector2.one;
    public GameObject mainPS;
    public GameObject impactPS;
}
