using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    public Save save;

    private void Awake()
    {
        instance = this;
    }
}
