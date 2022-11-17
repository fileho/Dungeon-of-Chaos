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
        return this;
    }

    public override void Drop()
    {
        foreach (LootItem item in lootTable)
        {
            float rnd = Random.Range(0f, 1f);
            float chance = item.prefab.GetComponent<Essence>().GetChance(owner) * item.weight;
            if (rnd <= chance)
                Spawn(item.prefab);
        }
    }

    private void Spawn(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity).GetComponent<Essence>().SetValue(owner);
    }
}
