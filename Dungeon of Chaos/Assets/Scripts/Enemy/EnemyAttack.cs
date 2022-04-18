using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyAttack : MonoBehaviour
{
    public float delay = 0.5f;
    public float cooldown = 1f;
    [SerializeField] protected float range = 10f;
    [SerializeField] protected float damage = 10f;

    public bool CanUse(Vector3 position)
    {
        float dist = ((Vector2) Character.instance.transform.position - (Vector2) position).magnitude;
        return dist <= range;
    }
}
