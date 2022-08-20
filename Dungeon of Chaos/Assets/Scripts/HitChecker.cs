using UnityEngine;

public class HitChecker : MonoBehaviour
{
    private Weapon weapon;

    private void Start()
    {
        weapon = GetComponentInParent<Weapon>();
    }

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.GetComponent<Unit>()) {
            weapon.InflictDamage(col.GetComponent<Unit>());
        }
    }

}
