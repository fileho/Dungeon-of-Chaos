using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Chest : MonoBehaviour, IMapSavable
{
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
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        if (((Vector2)transform.position - (Vector2)Character.instance.transform.position).magnitude < range)
            OpenBox();
    }

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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        tooltipCanvas.SetActive(false);
    }

    private void OpenBox()
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

    private void DrawOpened()
    {
        isOpened = true;
        spriteRenderer.sprite = openedSprite;
        shadowClosed.SetActive(false);
        shadowOpened.SetActive(true);
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
