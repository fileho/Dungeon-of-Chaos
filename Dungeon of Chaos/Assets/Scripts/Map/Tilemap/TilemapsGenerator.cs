using System.Collections.Generic;
using UnityEngine;
using hwfc;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Generates tilemaps from GameObject[,] representation
/// </summary>
public class TilemapsGenerator : MonoBehaviour
{
    [SerializeField]
    private Tilemaps tilemaps;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            FillTilemaps();
    }

    /// <summary>
    /// Fills tilemaps with corresponding tiles
    /// </summary>
    public void FillTilemaps()
    {
        GameObject[,] tiles = GetComponent<Postprocessing>().tiles;
        if (tiles == null)
            return;

        foreach (var map in tilemaps.GetAllMaps())
        {
            map.ClearAllTiles();
            map.transform.position = Vector3.zero;
        }

        // Loop over tiles
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                // There can be more than 1 tile at each position
                var et = GetExportTile(tiles[i, j]);
                foreach (var o in et)
                {
                    o.Place(tilemaps, new Vector3Int(i, j, 0));
                }
            }
        }

        foreach (var map in tilemaps.GetAllMaps())
            map.transform.Translate(0, 0, 20);

        tilemaps.ground.transform.Translate(0, 0, 1);
        Debug.Log("Tilemaps export done");
    }

    /// <summary>
    /// Checks an object and its children
    /// </summary>
    private static List<ExportTile> GetExportTile(GameObject go)
    {
        List<ExportTile> ret = new List<ExportTile>();
        if (go == null)
            return ret;

        var e = go.GetComponent<ExportTile>();
        if (e != null)
            ret.Add(e);
        for (int i = 0; i < go.transform.childCount; i++)
        {
            var child = go.transform.GetChild(i).GetComponent<ExportTile>();
            if (child != null)
                ret.Add(child);
        }
        return ret;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(TilemapsGenerator))]
public class TilemapsGeneratorEditor : Editor
{
    private SerializedProperty tilemaps;

    private void OnEnable()
    {
        tilemaps = serializedObject.FindProperty("tilemaps");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (tilemaps == null)
            OnEnable();

        if (GUILayout.Button("Export tiles"))
        {
            var generator = target as TilemapsGenerator;
            generator.FillTilemaps();
        }

        EditorGUILayout.PropertyField(tilemaps);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
