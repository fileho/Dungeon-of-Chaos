using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Vector3 = UnityEngine.Vector3;


// Generated all the shadow casters 2D for walls
[ExecuteInEditMode]
public class ShadowsGenerator : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    private HashSet<Vector3Int> visited;

    public void BakeShadows()
    {
        Setup();
        BindingFlags accessFlagsPrivate = BindingFlags.NonPublic | BindingFlags.Instance;
        while (true)
        {
            var f = FindFilledArea();
            // All shadows are constructed
            if (!f.HasValue)
                break;

            var o = new GameObject("ShadowCaster");
            o.transform.parent = transform;
            ShadowCaster2D shadowCaster2D = o.AddComponent<ShadowCaster2D>();

            // Use reflection since m_ShapePath is private without any wrappers
            // It is still in experimental phase
            FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);
            shapePathField.SetValue(shadowCaster2D, TrackEdges(f.Value));
        }

        // scene has to be reload for the shadows to rebuild
#if UNITY_EDITOR
        EditorSceneManager.OpenScene(SceneManager.GetActiveScene().path); 
#endif
    }

    private void Setup()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
        tilemap.CompressBounds();
        visited = new HashSet<Vector3Int>();
    }

    // Finds all corners for the shadow generation
    Vector3[] TrackEdges(Vector3Int start)
    {
        // use two lists, one for inner point, the other for outer point
        List<Vector3> inner = new List<Vector3>();
        List<Vector3> outer = new List<Vector3>();

        Vector3Int dir = Vector3Int.right;
        Vector3Int current = TrackEdge(start, ref dir, inner, outer);

        while (current != start)
        {
            current = TrackEdge(current, ref dir, inner, outer);
        }
        TrackEdge(current, ref dir, inner, outer);

        outer.Reverse();
        inner.AddRange(outer);

        return inner.ToArray();
    }

    private Vector3Int TrackEdge(Vector3Int current, ref Vector3Int dir, List<Vector3> inner, List<Vector3> outer)
    {
        while (tilemap.HasTile(current + dir))
        {
            current += dir;
            visited.Add(current);
        }

        var nextDir = GetNextDir(current, dir);
        AddCorners(current, dir, nextDir, inner, outer);
        dir = nextDir;

        return current;
    }

    private Vector3Int? FindFilledArea()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos) && !visited.Contains(pos))
                return pos;
        }

        return null;
    }

    Vector3Int GetNextDir(Vector3Int pos, Vector3Int dir)
    {
        var orthogonal = new Vector3Int(dir.y, dir.x, 0);
        return tilemap.HasTile(pos + orthogonal) ? orthogonal : -orthogonal;
    }

    void AddCorners(Vector3Int pos, Vector3Int oldDir, Vector3Int newDir, List<Vector3> inner, List<Vector3> outer)
    {
        const int scale = 4;

        var mid = (pos + new Vector3(0.5f, 0.5f)) * scale;
        var p1 = mid + 0.5f * scale * (Vector3)(newDir - oldDir);
        var p2 = mid + 0.5f * scale * (Vector3)(oldDir - newDir);

        if (inner.Count == 0)
        {
            inner.Add(p1);
            outer.Add(p2);
            return;
        }

        var tmp = inner[inner.Count - 1];
        const float limit = 0.1f;
        // find next point to add to each list
        if (oldDir.y != 0)
        {
            if (Math.Abs(tmp.x - p1.x) < limit)
            {
                inner.Add(p1);
                outer.Add(p2);
            }
            else
            {
                inner.Add(p2);
                outer.Add(p1);
            }
        }
        else
        {
            if (Math.Abs(tmp.y - p1.y) < limit)
            {
                inner.Add(p1);
                outer.Add(p2);
            }
            else
            {
                inner.Add(p2);
                outer.Add(p1);
            }
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ShadowsGenerator))]
public class ShadowsGeneratorEditor : Editor
{
    private ShadowsGenerator generator;
    private SerializedProperty tilemap;

    private void OnEnable()
    {
        generator = (ShadowsGenerator)target;
        tilemap = serializedObject.FindProperty("tilemap");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(tilemap);

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
