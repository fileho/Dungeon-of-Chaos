using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

[ExecuteInEditMode]
public class IdGenerator : MonoBehaviour
{
    private int id = 0;
    public void AssignIds()
    {
        id = 0; 
        SetIds(FindObjectsOfType<Checkpoint>());
        SetIds(FindObjectsOfType<MapFragment>());
        SetIds(FindObjectsOfType<Chest>());
    }

    private int GenerateNextId()
    {
        return ++id;
    }

    private void SetIds<T>(IEnumerable<T> list)
        where T : IMapSavable
    {
        foreach (var elem in list)
        {
            elem.SetUniqueId(GenerateNextId());
            // Set the component dirty to save changes
            EditorUtility.SetDirty(elem.GetAttachedComponent());
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
