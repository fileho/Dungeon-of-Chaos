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
    [SerializeField] private float value;

    [SerializeField]
    [ReadOnly]
    private int id;

    [Header("SFX")]
    [SerializeField] private SoundSettings chestOpen;

    private SaveSystem saveSystem;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id not set");
        saveSystem = FindObjectOfType<SaveSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(chestOpen);
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
        {
            if (loot.GetComponent<Essence>())
                Instantiate(loot, transform.position, Quaternion.identity).GetComponent<Essence>().SetValue(value);
            else
                Instantiate(loot, transform.position, Quaternion.identity);
        }
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
