using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MapFragment : MonoBehaviour, IMapSavable
{
    [SerializeField]
    [ReadOnly]
    private int id;

    private new GameObject light;
    private SaveSystem saveSystem;
    private GameObject tooltipCanvas;
    [SerializeField]
    private float range = 2.5f;

    private void Awake()
    {
        light = transform.Find("Light").gameObject;
        light.SetActive(false);
    }

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id is unassigned");
        tooltipCanvas = GetComponentInChildren<Canvas>().gameObject;
        tooltipCanvas.SetActive(false);

        saveSystem = FindObjectOfType<SaveSystem>();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.F))
            return;

        if (((Vector2)transform.position - (Vector2)Character.instance.transform.position).magnitude < range)
            Interact();
    }

    public void Interact()
    {
        saveSystem.DungeonData.AddSavedUid(id);
        Load();
        tooltipCanvas.SetActive(false);
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
    }

    public Object GetAttachedComponent()
    {
        return this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
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
}
