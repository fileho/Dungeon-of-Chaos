using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Chest : MonoBehaviour, IMapSavable
{
    [SerializeField]
    private GameObject loot;
    [SerializeField]
    private int lootCount = 1;

    [SerializeField]
    [ReadOnly]
    private int id;

    private SaveSystem saveSystem;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id not set");
        saveSystem = FindObjectOfType<SaveSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyBox();
    }

    private void DestroyBox()
    {
        DropLoot();
        var ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
        Invoke(nameof(CleanUp), ps.main.duration);
    }

    private void DropLoot()
    {
        for (int i = 0; i < lootCount; i++)
            Instantiate(loot, transform.position, Quaternion.identity);
        saveSystem.DungeonData.AddSavedUid(id);
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    public void SetUniqueId(int uid)
    {
        id = uid;
    }

    public int GetUniqueId()
    {
        return id;
    }

    public void Load()
    {
        CleanUp();
    }

    public Object GetAttachedComponent()
    {
        return this;
    }
}
