using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlots : MonoBehaviour
{
    private SaveSystem saveSystem;
    public List<Button> buttons;

    void Start()
    {
        saveSystem = FindObjectOfType<SaveSystem>();

        for (int i = 0; i < buttons.Count; i++)
        {
            var index = i;
            buttons[i].onClick.AddListener(delegate { ButtonClick(index); });
        }

        DrawInfo(0);
        DrawInfo(1);
        DrawInfo(2);
    }

    private void DrawInfo(int index)
    {
        var saveData = saveSystem.GetSavedData(index);
        DrawInfo(saveData, buttons[index].GetComponentInChildren<Text>());
    }

    private string GetOutputText(SaveData data)
    {
        if (data == null)
            return "Empty Save Slot";

        string s = "Dungeon " + data.dungeonData.dungeon + '\n' + "Level " + data.savedStats.savedLevelling.level +
                   '\n' + data.timestamp;
        return s;
    }

    private void DrawInfo(SaveData data, Text target)
    {
        string output = GetOutputText(data);
        target.text = output;
    }

    private void ButtonClick(int index)
    {
        saveSystem.SetSaveSlot(index);
        var saveData = saveSystem.GetSavedData(index);

        int load = saveData?.dungeonData.dungeon - 1 ?? 0;
        SceneManager.LoadScene(load + SaveSystem.SceneOffset);
    }
}
