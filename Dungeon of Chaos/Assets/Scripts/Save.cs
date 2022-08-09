using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SO/Save")]
public class Save : ScriptableObject
{
    private const int sceneOffset = 0;

    public int dungeon = 1;

    [SerializeField] private Vector3 characterPosition;


    public void LoadLevel()
    {
        SceneManager.LoadScene(dungeon + sceneOffset);
        
    }

    public void SavePosition(Vector3 pos)
    {
        characterPosition = pos;
    }

    public void MoveCharacter()
    {
        Character.instance.transform.position = characterPosition;
    }



    public void ResetData()
    {
        dungeon = 1;
    }
}