using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MapFragment : MonoBehaviour, IMapSavable
{
    [SerializeField]
#if UNITY_EDITOR
    [ReadOnly]
#endif
    private int id;

    private new GameObject light;
    private SaveSystem saveSystem;

    [SerializeField]
    private SoundSettings pickupSFX;

    private void Awake()
    {
        light = transform.Find("Light").gameObject;
        light.SetActive(false);
    }

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id is unassigned");
        saveSystem = FindObjectOfType<SaveSystem>();
    }

    public void Interact()
    {
        SoundManager.instance.PlaySound(pickupSFX);
        saveSystem.DungeonData.AddSavedUid(id);
        FindObjectOfType<TooltipSystem>().ShowMessage("Map Revealed", 2f);
        Load();
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
        light.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public Object GetAttachedComponent()
    {
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}
