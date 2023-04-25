using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Book that allows resetting unlocked of skills
/// </summary>
public class ResetBook : MonoBehaviour, IMapSavable
{
    [SerializeField]
#if UNITY_EDITOR
    [ReadOnly]
#endif
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

    // Interface for saves
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
        if (!collision.collider.CompareTag("Player"))
            return;

        collision.gameObject.GetComponent<Character>().stats.ColectReset();
        Interact();
        CleanUp();
    }
}
