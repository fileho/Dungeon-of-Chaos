using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.Video;

public class RockShadows : MonoBehaviour
{
    [System.Serializable]
    struct RockShadow
    {
        public TileBase rock;
        public ShadowCaster2D shadow;
    }

    [SerializeField]
    private List<RockShadow> rockShadows;

    /// <summary>
    /// Uses a hand made shadow casters because the shapes of rocks are quite complex. It will only spawn corresponding
    /// shadow caster over each rock.
    /// </summary>
    public void PlaceShadows(Tilemap tilemap, float scale)
    {
        const string transformName = "Rocks";
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
