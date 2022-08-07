using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Loot/Loot")]
public class Loot : ILoot
{
    [SerializeField] private List<LootItem> lootTable;

    private Transform transform;
    private float totalWeight;
    public override ILoot Init(Transform transform)
    {
        this.transform = transform;
        totalWeight = lootTable.Aggregate(0f, (sum, next) => sum += next.weight);

        return this;
    }

    public override void Drop()
    {
        float rnd = Random.Range(0, totalWeight);

        float sum = 0;
        foreach (LootItem item in lootTable)
        {
            sum += item.weight;
            if (!(sum >= rnd)) continue;
            Spawn(item.prefab);
            return;
        }
    }

    private void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity);
    }
}
