using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Loot/Loot")]
public class Loot : ILoot
{
    [SerializeField] private List<LootItem> lootTable;

    private Transform transform;
    private Enemy owner;

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
            if (item.prefab.GetComponent<Essence>() != null)
            {
                float rnd = Random.Range(0f, 1f);
                float chance = item.prefab.GetComponent<Essence>().GetChance(owner) * item.weight;
                if (rnd <= chance)
                    SpawnEssence(item.prefab);
            }
            else
                Instantiate(item.prefab, transform.position, Quaternion.identity);
        }
    }

    private void SpawnEssence(GameObject go)
    {
        Instantiate(go, transform.position, Quaternion.identity).GetComponent<Essence>().SetValue(owner);
    }
}
