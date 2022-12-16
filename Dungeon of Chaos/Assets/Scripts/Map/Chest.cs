using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Chest : MonoBehaviour, IMapSavable
{
    [SerializeField]
    private Sprite openedSprite;
    [SerializeField]
    private GameObject loot;
    [SerializeField]
    private int lootCount = 1;
    [SerializeField]
    private float value;

    [SerializeField]
    [ReadOnly]
    private int id;

    [Header("SFX")]
    [SerializeField]
    private SoundSettings chestOpen;

    private SaveSystem saveSystem;
    private SpriteRenderer spriteRenderer;
    private bool isOpened;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id not set");
        spriteRenderer = GetComponent<SpriteRenderer>();
        saveSystem = FindObjectOfType<SaveSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpened)
            return;
        SoundManager.instance.PlaySound(chestOpen);
        OpenBox();
    }

    private void OpenBox()
    {
        DropLoot();
        isOpened = true;
        spriteRenderer.sprite = openedSprite;
        var ps = GetComponentInChildren<ParticleSystem>();
        ps.Play();
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

    private void DrawOpened()
    {
        isOpened = true;
        spriteRenderer.sprite = openedSprite;
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
        DrawOpened();
    }

    public Object GetAttachedComponent()
    {
        return this;
    }
}
