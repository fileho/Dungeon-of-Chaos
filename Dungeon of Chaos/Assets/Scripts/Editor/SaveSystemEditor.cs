using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    private SaveSystem saveSystem;
    private SerializedProperty defaultCharacterPositions;

    private void OnEnable()
    {
        saveSystem = (SaveSystem)target;
        defaultCharacterPositions = serializedObject.FindProperty("defaultCharacterPositions");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Remove current save"))
        {
            saveSystem.RemoveSave();
        }

        EditorGUILayout.PropertyField(defaultCharacterPositions);

        serializedObject.ApplyModifiedProperties();
    }
}
