using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom editor to make loot definition simpler
/// </summary>
[CustomEditor(typeof(Loot))]
public class LootEditor : Editor
{
    private SerializedProperty lootTable;

    private void OnEnable()
    {
        lootTable = serializedObject.FindProperty("lootTable");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        for (int i = 0; i < lootTable.arraySize; i++)
        {

            if (!DrawProperty(lootTable.GetArrayElementAtIndex(i)))
                continue;
            lootTable.DeleteArrayElementAtIndex(i);
        }

        if (GUILayout.Button("ADD Loot item"))
            lootTable.InsertArrayElementAtIndex(lootTable.arraySize);

        serializedObject.ApplyModifiedProperties();
    }

    private bool DrawProperty(SerializedProperty p)
    {
        // make the with short so it fits on one line
        EditorGUIUtility.labelWidth = 70;
        EditorGUILayout.BeginHorizontal();

        var val = p.FindPropertyRelative("prefab");
        var h = p.FindPropertyRelative("weight");

        EditorGUILayout.PropertyField(val);

        EditorGUILayout.PropertyField(h);

        if (GUILayout.Button("Remove"))
        {
            EditorGUILayout.EndHorizontal();
            return true;
        }

        EditorGUILayout.EndHorizontal();

        return false;
    }
}
