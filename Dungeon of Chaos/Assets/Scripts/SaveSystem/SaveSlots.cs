using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SaveSlots : MonoBehaviour
{
    private SaveSystem saveSystem;
    [SerializeField]
    private List<Button> slots;
    [SerializeField]
    private List<Button> deletes;

    void Start()
    {
        CultureInfo.CurrentCulture = new CultureInfo("cs-cz");

        saveSystem = FindObjectOfType<SaveSystem>();

        for (int i = 0; i < slots.Count; i++)
        {
            var index = i;
            slots[i].onClick.AddListener(delegate { ButtonClick(index); });
        }

        DrawInfo(0);
        DrawInfo(1);
        DrawInfo(2);
    }

    private void DrawInfo(int index)
    {
        var saveData = saveSystem.GetSavedData(index);
        DrawInfo(saveData, slots[index].transform.GetChild(0), deletes[index].transform);
    }

    private void DrawInfo(SaveData data, Transform target, Transform delete)
    {
        var dungeon = target.GetChild(0).GetComponent<TMP_Text>();
        var level = target.GetChild(1).GetComponent<TMP_Text>();
        var time = target.GetChild(2).GetComponent<TMP_Text>();

        if (data == null)
        {
            level.gameObject.SetActive(false);
            time.gameObject.SetActive(false);
            var cg = delete.GetComponent<CanvasGroup>();
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
            return;
        }

        dungeon.text = "Dungeon " + data.dungeonData.dungeon;
        level.text = "Level " + data.savedStats.savedLevelling.level;
        time.text = data.timestamp.ToString();
    }

    private void ButtonClick(int index)
    {
        saveSystem.SetSaveSlot(index);
        var saveData = saveSystem.GetSavedData(index);

        int load = saveData?.dungeonData.dungeon - 1 ?? 0;
        SceneManager.LoadScene(load + SaveSystem.SceneOffset);
    }
}
