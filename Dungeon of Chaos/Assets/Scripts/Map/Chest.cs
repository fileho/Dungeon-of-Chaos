using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Openable check the drops loot
/// </summary>
public class Chest : MonoBehaviour, IMapSavable
{
    // Interaction range
    [SerializeField]
    private float range = 2.5f;
    [SerializeField]
    private Sprite openedSprite;
    [SerializeField]
    private GameObject loot;
    [SerializeField]
    private int lootCount = 1;
    [SerializeField]
    private float value;

    [SerializeField]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    private int id;

    [Header("SFX")]
    [SerializeField]
    private SoundSettings chestOpen;

    private SaveSystem saveSystem;
    private SpriteRenderer spriteRenderer;
    private GameObject shadowClosed;
    private GameObject shadowOpened;
    private GameObject tooltipCanvas;

    private bool isOpened;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id not set");
        spriteRenderer = GetComponent<SpriteRenderer>();
        saveSystem = FindObjectOfType<SaveSystem>();
        shadowClosed = transform.Find("ShadowClosed").gameObject;
        shadowOpened = transform.Find("ShadowOpened").gameObject;
        shadowOpened.SetActive(false);
        tooltipCanvas = transform.Find("Canvas").gameObject;
        tooltipCanvas.SetActive(false);
    }

    private void Update()
    {
        // Handle interaction
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        if (((Vector2)transform.position - (Vector2)Character.instance.transform.position).magnitude < range)
            OpenChest();
    }

    // Show tooltip
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpened || !collision.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Hide tooltip
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(false);
    }

    private void OpenChest()
    {
        if (isOpened)
            return;
        tooltipCanvas.SetActive(false);
        SoundManager.instance.PlaySound(chestOpen);
        DropLoot();
        DrawOpened();
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

    /// <summary>
    /// Change the sprite and shadow caster for the chest
    /// </summary>
    private void DrawOpened()
    {
        isOpened = true;
        spriteRenderer.sprite = openedSprite;
        shadowClosed.SetActive(false);
        shadowOpened.SetActive(true);
    }

    // Interface for saves
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
