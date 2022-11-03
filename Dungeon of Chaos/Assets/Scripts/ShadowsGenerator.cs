using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Vector3 = UnityEngine.Vector3;

[ExecuteInEditMode]
public class ShadowsGenerator : MonoBehaviour
{
    private ShadowCaster2D shadowCaster2D;
    [SerializeField]
    private Tilemap tilemap;

    public void BakeShadows()
    {
        shadowCaster2D = GetComponent<ShadowCaster2D>();
        BindingFlags accessFlagsPrivate = BindingFlags.NonPublic | BindingFlags.Instance;
        FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", accessFlagsPrivate);
        shapePathField.SetValue(shadowCaster2D, TrackEdges());
    }

    Vector3[] TrackEdges()
    {
        List<Vector3> inner = new List<Vector3>();
        List<Vector3> outer = new List<Vector3>();

        Vector3Int start = FindFilledArea();

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
        }

        var nextDir = GetNextDir(current, dir);
        AddCorners(current, dir, nextDir, inner, outer);
        dir = nextDir;

        return current;
    }

    private Vector3Int FindFilledArea()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
                return pos;
        }

        throw new Exception("No tile found");
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
        var p1 = mid + (Vector3)(newDir - oldDir) * 0.5f * scale;
        var p2 = mid + (Vector3)(oldDir - newDir) * 0.5f * scale;

        if (inner.Count == 0)
        {
            inner.Add(p1);
            outer.Add(p2);
            return;
        }

        var tmp = inner[inner.Count - 1];
        const float limit = 0.1f;

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

        if (GUILayout.Button("Bake Shadows"))
        {
            generator.BakeShadows();
        }

        EditorGUILayout.PropertyField(tilemap);

        serializedObject.ApplyModifiedProperties();
    }
}

#endif
