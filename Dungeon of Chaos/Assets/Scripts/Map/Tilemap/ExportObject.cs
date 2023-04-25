using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Maps prefab to GameObject to be exported
/// Used for object with a runtime behavior like torches
/// </summary>
public class ExportObject : ExportTile
{
    [SerializeField]
    private GameObject objectToSpawn;

    public override void Place(Tilemaps maps, Vector3Int pos)
    {
        float scale = maps.objects.parent.localScale.x;
        Instantiate(objectToSpawn, (pos + new Vector3(0.5f, 0.5f, 0)) * scale, Quaternion.identity, maps.objects);
    }
}
