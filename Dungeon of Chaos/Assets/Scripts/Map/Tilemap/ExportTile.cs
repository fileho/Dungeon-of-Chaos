using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Maps each prefab to a tiles and a tilemap
/// </summary>
public class ExportTile : MonoBehaviour
{
    [SerializeField]
    private TileBase tile;

    [SerializeField]
    private Tilemaps.TilemapType tilemapType;

    public virtual void Place(Tilemaps maps, Vector3Int pos)
    {
        var map = maps.SelectMap(tilemapType);

        map.SetTile(pos, tile);
    }
}
