using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Helper script to be called in the editor
/// </summary>
public class EditorScripts
{
    private static int id = 0;
    [MenuItem("Dungeon/Assign Ids")]
    public static void AssignIds()
    {
        id = 0;
        SetIds(Editor.FindObjectsOfType<Checkpoint>());
        SetIds(Editor.FindObjectsOfType<MapFragment>());
        SetIds(Editor.FindObjectsOfType<Chest>());
        SetIds(Editor.FindObjectsOfType<ResetBook>());
    }

    private static int GenerateNextId()
    {
        return ++id;
    }

    private static void SetIds<T>(IEnumerable<T> list)
        where T : IMapSavable
    {
        foreach (var elem in list)
        {
            elem.SetUniqueId(GenerateNextId());
            // Set the component dirty to save changes
            EditorUtility.SetDirty(elem.GetAttachedComponent());
        }
    }

    /// <summary>
    /// Torches are now generated as prefabs, this helper allows us to replace them
    /// </summary>
    [MenuItem("Dungeon/Replace Torches")]

    public static void ReplaceTorches()
    {
        ReplaceTorches("Assets/Prefabs/Map/Torch.prefab");
    }

    /// <summary>
    /// Torches are now generated as prefabs, this helper allows us to replace them for blue torches
    /// </summary>
    [MenuItem("Dungeon/Replace Torches Blue")]
    public static void ReplaceTorchesBlue()
    {
        ReplaceTorches("Assets/Prefabs/Map/TorchBlue.prefab");
    }

    [MenuItem("Save/Reset Player Prefs")]
    public static void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Save/Delete All Saves")]
    public static void DeleteAllSaves()
    {
        var saveSystem = Editor.FindObjectOfType<SaveSystem>();
        if (saveSystem != null)
        {
            saveSystem.RemoveAllSaves();
            Debug.Log("All saved were removed");
        }
    }

    private static void ReplaceTorches(string torchPath)
    {
        GameObject assetRoot = Selection.activeObject as GameObject;
        string assetPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(assetRoot);
        GameObject contentsRoot = PrefabUtility.LoadPrefabContents(assetPath);

        foreach (var torch in contentsRoot.GetComponentsInChildren<Torch>())
        {
            var pos = torch.transform.position;

            var t = AssetDatabase.LoadAssetAtPath(torchPath, typeof(GameObject));
            var go = PrefabUtility.InstantiatePrefab(t, torch.transform.parent) as GameObject;
            go.transform.position = pos;

            Editor.DestroyImmediate(torch.gameObject, true);
        }

        PrefabUtility.SaveAsPrefabAsset(contentsRoot, assetPath);
        PrefabUtility.UnloadPrefabContents(contentsRoot);
    }
}
