using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LoadGameUIManager : MonoBehaviour
{
    [Header("=== Load Game Menu ===")]
    [SerializeField] Button[] loadSlots;


    private UnityAction[] actions;

    private void OnEnable()
    {
        actions = new UnityAction[loadSlots.Length];
        for (int i = 0; i < loadSlots.Length; i++)
        {
            // Storing index in a local variable since closures reference the same copy of the i variable (last value)
            int index = i;
            actions[index] = () => { OnSlotPressed(index); };
            loadSlots[index].onClick.AddListener(actions[index]);
        }
    }


    private void OnDisable()
    {
        for (int i = 0; i < loadSlots.Length; i++)
        {
            loadSlots[i].onClick.RemoveListener(actions[i]);
        }
    }


    void OnSlotPressed(int slotIndex)
    {
        UIEvents.LoadSlotPressed?.Invoke(slotIndex);
        print("LoadSlotIndex: " + slotIndex);
    }
}
