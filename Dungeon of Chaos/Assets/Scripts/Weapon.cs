using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public class Weapon : MonoBehaviour {

    [Tooltip("Angle at which the weapon asset aligns with the Y axis")]
    [SerializeField] private float upRightAngle = 0f;

    [Tooltip("Angle at which the weapon asset aligns with the arm")]
    [SerializeField] private float armAlignAngle = 0f;

    [Tooltip("Local position of weaapon tip")]
    [SerializeField] private Vector3 weaponTipOffset = Vector3.zero;

    private float weaponAssetWidth;
    public float WeaponAssetWidth {
        get {
            return weaponAssetWidth != 0 ? weaponAssetWidth : Asset.GetComponent<SpriteRenderer>().bounds.size.x /2f;
        }
    }


    private Transform asset;
    public Transform Asset {
        get {
            return asset != null ? asset : (asset = transform.Find("Asset"));
        }
    }


    private TrailRenderer trail;
    private new BoxCollider2D collider;
    private List<Unit> hitUnits;
    private float damage = 0;
    private int enemyLayer;


    private SoundSettings impactSFX;

    void Start() {
        trail = GetComponentInChildren<TrailRenderer>();
        collider = GetComponentInChildren<BoxCollider2D>();
        hitUnits = new List<Unit>();
        EnableDisableCollider(false);
        EnableDisableTrail(false);
        enemyLayer = GetEnemyLayer(transform.parent.gameObject.layer);
    }


    // Damage is set by the attack as different attacks can have different damage for the same weapon.
    public void SetDamage(float d) {
        damage = d;
    }

    // Impact sound is set by the attack as different attacks can have different impact sounds
    public void SetImpactSound(SoundSettings sound) {
        impactSFX = sound;
    }

    public float GetUprightAngle() {
        return upRightAngle;
    }

    public float GetArmAlignAngle() {
        return armAlignAngle;
    }

    public Vector3 GetWeaponTipOffset() {
        return weaponTipOffset;
    }

    public void InflictDamage(Unit unit) {
        if (unit.gameObject.layer == enemyLayer && !hitUnits.Contains(unit)) {
            hitUnits.Add(unit);
            SoundManager.instance.PlaySound(impactSFX);
            unit.TakeDamage(damage);
        }
    }

    public void EnableDisableCollider(bool state) {
        collider.enabled = state;
    }

    public void EnableDisableTrail(bool state) {
        trail.gameObject.SetActive(state);
    }

    public void ResetHitUnits() {
        hitUnits.Clear();
    }


    public void RotateWeapon(Vector2 target) {
        Vector2 dir = (target - (Vector2)transform.position).normalized;

        if (transform.lossyScale.x > 0)
            dir *= -1;


        Vector3 rotated = Quaternion.Euler(0, 0, 90) * dir;

        var q = Quaternion.LookRotation(Vector3.forward, rotated);

        var e = q.eulerAngles;

        transform.rotation = Quaternion.Euler(e);
    }


    // Resets weapon to the default position
    public void ResetWeapon() {
        RotateWeapon(transform.position);
    }


    public Vector3 GetForwardDirection() {
        float dir = transform.lossyScale.x > 0 ? 1 : -1;

        float a = transform.rotation.eulerAngles.z * dir;
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

    public int GetEnemyLayer(int ownerLayer) {
        return (ownerLayer == LayerMask.NameToLayer("Enemy") || ownerLayer == LayerMask.NameToLayer("EnemyAttack"))
            ? LayerMask.NameToLayer("Player")
            : LayerMask.NameToLayer("Enemy");
    }
}
