using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;

/// <summary>
/// Places correct shadows shape over each rock
/// </summary>
public class RockShadows : MonoBehaviour
{
    // Mapping from Tile to shadow shape
    [System.Serializable]
    struct RockShadow
    {
        // ReSharper disable once UnassignedField.Local
        public TileBase rock;
        // ReSharper disable once UnassignedField.Local
        public ShadowCaster2D shadow;
    }

    // All static object to spawn shadows
    [SerializeField]
    private List<RockShadow> rockShadows;

    /// <summary>
    /// Uses a hand made shadow casters because the shapes of rocks are quite complex. It will only spawn corresponding
    /// shadow caster over each rock.
    /// </summary>
    public void PlaceShadows(Tilemap tilemap, float scale)
    {
        const string transformName = "Rocks";
        var t = transform.Find(transformName);
        if (t != null)
            DestroyImmediate(t.gameObject);

        var r = new GameObject(transformName).transform;
        r.parent = transform;

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            var tile = tilemap.GetTile(pos);
            if (tile == null)
                continue;

            var found = rockShadows.Find((shadow => shadow.rock == tile));
            var o = Instantiate(found.shadow, (pos + new Vector3(0.5f, 0.5f)) * scale, Quaternion.identity, r);
            o.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
