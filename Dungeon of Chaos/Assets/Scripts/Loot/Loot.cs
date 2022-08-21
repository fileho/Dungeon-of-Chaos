using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Loot/Loot")]
public class Loot : ILoot
{
    [SerializeField] private List<LootItem> lootTable;

    private Transform transform;
    private Enemy owner;
    //private float totalWeight;
    public override ILoot Init(Enemy enemy)
    {
        this.transform = enemy.transform;
        this.owner = enemy;
        //totalWeight = lootTable.Aggregate(0f, (sum, next) => sum += next.weight);

        return this;
    }

    public override void Drop()
    {
        /*float rnd = Random.Range(0, totalWeight);

        float sum = 0;
        foreach (LootItem item in lootTable)
        {
            sum += item.weight;
            if (!(sum >= rnd)) continue;
            Spawn(item.prefab);
            return;
        }*/

        foreach (LootItem item in lootTable)
        {
            float rnd = Random.Range(0, 1);
            float chance = item.prefab.GetComponent<Essence>().GetChance(owner);
            if (rnd <= chance)
                Spawn(item.prefab);
        }
    }

    private void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity).GetComponent<Essence>().SetValue(owner);
    }
}
