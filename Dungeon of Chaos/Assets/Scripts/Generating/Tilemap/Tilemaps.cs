using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct Tilemaps
{
    public Tilemap ground;
    public Tilemap wall;
    public Tilemap decorations;
    public Tilemap rocks;
    public Transform objects;

    public Tilemap SelectMap(TilemapType type)
    {
        return type switch
        {
            TilemapType.Ground => ground,
            TilemapType.Walls => wall,
            TilemapType.Decorations => decorations,
            TilemapType.Rocks => rocks,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public Tilemap[] GetAllMaps()
    {
        return new[] {ground, wall, decorations};
    }

    public enum TilemapType
    {
        Ground,
        Walls,
        Decorations,
        Rocks
    }
}
