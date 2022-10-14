using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SaveSystem))]
public class SaveSystemEditor : Editor
{
    private SaveSystem saveSystem;

    private void OnEnable()
    {
        saveSystem = (SaveSystem) target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Remove current save"))
        {
            saveSystem.RemoveCurrentSave();
        }
        
        serializedObject.ApplyModifiedProperties();
    }
}
