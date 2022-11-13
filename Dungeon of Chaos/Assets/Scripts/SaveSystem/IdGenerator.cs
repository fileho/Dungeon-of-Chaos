using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class IdGenerator : MonoBehaviour
{
    private int id = 0;
    public void AssignIds()
    {
        id = 0;
        Checkpoints();
    }

    private int GenerateNextId()
    {
        return ++id;
    }

    private void Checkpoints()
    {
        var checkpoints = FindObjectsOfType<Checkpoint>();

        foreach (var checkpoint in checkpoints)
        {
            checkpoint.SetUniqueId(GenerateNextId());
            EditorUtility.SetDirty(checkpoint);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(IdGenerator))]
public class IdGeneratorEditor : Editor
{
    private IdGenerator generator;

    private void OnEnable()
    {
        generator = (IdGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Set unique IDs"))
        {
            generator.AssignIds();
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif