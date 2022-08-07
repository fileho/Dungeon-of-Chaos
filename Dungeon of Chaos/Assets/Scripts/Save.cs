using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SO/Save")]
public class Save : ScriptableObject
{
    private const int sceneOffset = -1;

    public int dungeon = 1;

    public Vector3 characterPosition;


    public void LoadLevel()
    {
        SceneManager.LoadScene(dungeon + sceneOffset);
        Character.instance.transform.position = characterPosition;
    }

    public void SavePosition(Vector3 pos)
    {
        characterPosition = pos;
    }



    public void ResetData()
    {
        dungeon = 1;
    }
}