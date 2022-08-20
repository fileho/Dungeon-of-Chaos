using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour {

    [Tooltip("Initial offset z angle for weapon to point right")]
    [SerializeField] private float angleOffset = 0f;


    private TrailRenderer trail;
    private new BoxCollider2D collider;
    private List<GameObject> hitUnits;
    private float damage = 0;


    void Start() {
        trail = GetComponentInChildren<TrailRenderer>();
        collider = GetComponentInChildren<BoxCollider2D>();
        EnableDisableCollider(false);
        EnableDisableTrail(false);
    }


    // Damage is set by the attack as different attacks can have different damage for the same weapon.
    public void SetDamage(float d) {
        damage = d;
    }


    public void InflictDamage(Unit unit) {
        unit.TakeDamage(damage);
    }

    public void EnableDisableCollider(bool state) {
        collider.enabled = state;
    }

    public void EnableDisableTrail(bool state) {
        trail.gameObject.SetActive(state);
    }


    public void RotateWeapon(Vector2 target) {
        Vector2 dir = (target - (Vector2)transform.position).normalized;

        if (transform.lossyScale.x > 0)
            dir *= -1;


        Vector3 rotated = Quaternion.Euler(0, 0, 90) * dir;

        var q = Quaternion.LookRotation(Vector3.forward, rotated);

        var e = q.eulerAngles;
        e.z += transform.lossyScale.x > 0 ? -angleOffset : angleOffset;

        transform.rotation = Quaternion.Euler(e);
    }


    // Resets weapon to the default position
    public void ResetWeapon() {
        RotateWeapon(transform.position);
    }


    public Vector3 GetForwardDirection() {
        float dir = transform.lossyScale.x > 0 ? 1 : -1;

        float a = angleOffset + transform.rotation.eulerAngles.z * dir;
        a *= Mathf.Deg2Rad;
        // Vector3.R


        return new Vector3(-Mathf.Cos(a), -Mathf.Sin(a), 0);
    }


    // Assumes the current scale of the Enemy, useful for attack indicators
    public Vector3 GetForwardDirectionRotated() {
        float dir = transform.lossyScale.x > 0 ? 1 : -1;

        var ret = GetForwardDirection();
        ret.x *= dir;

        return ret;
    }

}
