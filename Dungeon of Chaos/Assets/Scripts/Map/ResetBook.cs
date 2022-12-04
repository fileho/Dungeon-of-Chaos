using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ResetBook : MonoBehaviour, IMapSavable
{
    [SerializeField]
    [ReadOnly]
    private int id;

    private SaveSystem saveSystem;

    private void Start()
    {
        Assert.AreNotEqual(id, 0, "Unique id is unassigned");

        saveSystem = FindObjectOfType<SaveSystem>();
    }

    public void Interact()
    {
        saveSystem.DungeonData.AddSavedUid(id);
    }

    public void SetUniqueId(int uid)
    {
        id = uid;
    }

    public int GetUniqueId()
    {
        return id;
    }


    public Object GetAttachedComponent()
    {
        return this;
    }

    public void Load()
    {
        CleanUp();
    }

    private void CleanUp()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        Debug.Log("Collect");
        collision.gameObject.GetComponent<Character>().stats.ColectReset();
        CleanUp();
    }

}
