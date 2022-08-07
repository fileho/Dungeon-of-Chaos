using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private GameObject attack;
    [SerializeField] private float range = 3f;

    [SerializeField] private float delay = 0.2f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private int attackCount;
    public float delayBetweenAttacks = 0.2f;

    private Enemy enemy;
    private float cooldownLeft = 0f;

    private void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        if (cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }


    public float GetDelay()
    {
        return delay + attackCount * delayBetweenAttacks;
    }

    

    public bool CanUse(Vector3 position)
    {
        float dist = ((Vector2) Character.instance.transform.position - (Vector2) position).magnitude;
        return dist <= range && cooldownLeft <= 0;
    }

    public void Use()
    {
        cooldownLeft = cooldown;
        StartCoroutine(SpawnProjectiles());
    }


    private IEnumerator SpawnProjectiles()
    {
        for (int i = 0; i < attackCount; i++)
        {
            var o = Instantiate(attack, transform.position, transform.rotation, enemy.transform);

            IEnemy ie = o.GetComponent<IEnemy>();
            ie?.SetEnemy(enemy);


            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (Selection.activeGameObject != gameObject)
            return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
#endif
}
