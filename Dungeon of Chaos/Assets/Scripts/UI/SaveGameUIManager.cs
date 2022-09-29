using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SaveGameUIManager : MonoBehaviour
{
    [Header("=== Save Game Menu ===")]
    [SerializeField] Button[] loadSlots;


    private UnityAction[] actions;

    private void OnEnable() {
        actions = new UnityAction[loadSlots.Length];
        for (int i = 0; i < loadSlots.Length; i++) {
            actions[i] = () => { OnSlotPressed(i); };
            loadSlots[i].onClick.AddListener(actions[i]);
        }
    }


    private void OnDisable() {
        for (int i = 0; i < loadSlots.Length; i++) {
            loadSlots[i].onClick.RemoveListener(actions[i]);
        }
    }


    void OnSlotPressed(int slotIndex) {
        UIEvents.SaveSlotPressed?.Invoke(slotIndex);
        print("SaveSlotIndex: " + slotIndex);
    }
}
