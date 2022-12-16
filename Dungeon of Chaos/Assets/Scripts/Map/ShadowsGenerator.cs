using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using Vector3 = UnityEngine.Vector3;

// Generated all the shadow casters 2D for walls
[ExecuteInEditMode]
public class ShadowsGenerator : MonoBehaviour
{
    private Tilemap tilemap;
    private HashSet<Vector3Int> visited;

    private float scale = 1f;

    const string transformName = "Walls";

    public void BakeShadows()
    {

        var grid = FindObjectOfType<Grid>();
        tilemap = GetTilemap(grid, "Walls");

        Setup();
        Transform parent = transform.Find(transformName);
        BindingFlags accessFlagsPrivate = BindingFlags.NonPublic | BindingFlags.Instance;

        // Assume uniform scaling
        scale = tilemap.transform.lossyScale.x;

        while (true)
        {
            var f = FindFilledArea();
            // All shadows are constructed
            if (!f.HasValue)
                break;

            var o = new GameObject("ShadowCaster");
            o.transform.parent = parent;
            ShadowCaster2D shadowCaster2D = o.AddComponent<ShadowCaster2D>();

            // Use reflection since m_ShapePath is private without any wrappers
            // It is still in experimental phase
            FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);
            shapePathField.SetValue(shadowCaster2D, TrackEdges(f.Value));

            FieldInfo applyToSortingLayers =
                typeof(ShadowCaster2D).GetField("m_ApplyToSortingLayers", accessFlagsPrivate);
            applyToSortingLayers.SetValue(shadowCaster2D, SetDefaultSortingLayers());
        }

        // Spawn shadows for rocks
        GetComponent<RockShadows>().PlaceShadows(GetTilemap(grid, "Rocks"), scale);

        // Fog
        foreach (var fog in FindObjectsOfType<Fog>())
            fog.BakeShadows();

#if UNITY_EDITOR
        // scene has to be reload for the shadows to rebuild
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        EditorSceneManager.OpenScene(EditorSceneManager.GetActiveScene().path);
#endif
    }

    static int[] SetDefaultSortingLayers()
    {
        int layerCount = SortingLayer.layers.Length;
        int[] allLayers = new int[layerCount];

        for (int layerIndex = 0; layerIndex < layerCount; layerIndex++)
        {
            allLayers[layerIndex] = SortingLayer.layers[layerIndex].id;
        }

        return allLayers;
    }

    private Tilemap GetTilemap(Grid grid, string mapName)
    {
        Assert.IsNotNull(grid, "No tilemaps found");
        return grid.transform.Find(mapName).GetComponent<Tilemap>();
    }

    private void Setup()
    {
        Transform t = transform.Find(transformName);
        if (t != null)
            DestroyImmediate(t.gameObject);

        var go = new GameObject(transformName);
        go.transform.parent = transform;

        tilemap.CompressBounds();
        visited = new HashSet<Vector3Int>();
    }

    // Finds all corners for the shadow generation
    Vector3[] TrackEdges(Vector3Int start)
    {
        var points = new List<Vector3>();
        Vector3Int dir = Vector3Int.right;

        TrackEdge(start, dir, points);
        TrackEdge(start, -dir, points);

        return points.ToArray();
    }

    private void TrackEdge(Vector3Int pos, Vector3Int dir, List<Vector3> points)
    {
        var start = pos;
        var p = CalculatePosition(start, dir, GetNextDir(dir));
        points.Add(p);

        int iterations = 0;
        do
        {
            ++iterations;
            visited.Add(pos);
            var nextDir = GetNextDir(dir);
            if (tilemap.HasTile(pos + nextDir))
            {
                points.Add(CalculatePosition(pos, dir, nextDir));
                pos += nextDir;
                dir = nextDir;
                continue;
            }

            if (tilemap.HasTile(pos + dir))
            {
                pos += dir;
                continue;
            }

            nextDir = GetPrevDir(dir);
            if (tilemap.HasTile(pos + nextDir))
            {
                points.Add(CalculatePosition(pos, -dir, -nextDir));
                pos += nextDir;
                dir = nextDir;
            }
            else
            {
                points.Add(CalculatePosition(pos, -dir, GetNextDir(dir)));
                points.Add(CalculatePosition(pos, -dir, GetNextDir(-dir)));
                dir *= -1;
            }
        } while (pos != start && iterations < 2000);

        if (iterations == 2000)
            Debug.LogError("Shadow generation failed");

        points.Add(p);
    }

    private Vector3 CalculatePosition(Vector3 pos, Vector3 oldDir, Vector3 newDir)
    {
        var mid = (pos + new Vector3(0.5f, 0.5f)) * scale;
        mid += scale * 0.5f * newDir;
        mid -= scale * 0.5f * oldDir;

        return mid;
    }

    private Vector3Int? FindFilledArea()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos) || visited.Contains(pos))
                continue;
            if (tilemap.HasTile(pos + Vector3Int.left) && tilemap.HasTile(pos + Vector3Int.right))
                return pos;
        }

        return null;
    }

    static Vector3Int GetNextDir(Vector3Int dir)
    {
        if (dir == Vector3Int.right)
            return Vector3Int.down;
        if (dir == Vector3Int.down)
            return Vector3Int.left;
        if (dir == Vector3Int.left)
            return Vector3Int.up;
        return Vector3Int.right;
    }

    static Vector3Int GetPrevDir(Vector3Int dir)
    {
        if (dir == Vector3Int.right)
            return Vector3Int.up;
        if (dir == Vector3Int.down)
            return Vector3Int.right;
        if (dir == Vector3Int.left)
            return Vector3Int.down;
        return Vector3Int.left;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ShadowsGenerator))]
public class ShadowsGeneratorEditor : Editor
{
    private ShadowsGenerator generator;

    private void OnEnable()
    {
        generator = (ShadowsGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (GUILayout.Button("Bake Shadows"))
        {
            generator.BakeShadows();
            // need to return since the scene will be reopened from code to reload shadows
            return;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
