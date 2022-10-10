using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    public Save save;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        save.dungeon = SceneManager.GetActiveScene().buildIndex;
    }
}
