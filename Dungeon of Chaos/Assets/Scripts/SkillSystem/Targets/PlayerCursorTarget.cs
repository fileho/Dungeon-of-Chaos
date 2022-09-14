using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Skills/Targets/PlayerCursorTarget")]
public class PlayerCursorTarget : ITarget
{
    public override List<Vector2> GetTargetPositions()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Vector2 position = targettingData.owner.gameObject.layer == enemyLayer
            ? Character.instance.transform.position
            : Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        return new List<Vector2>() { position };
    }
}
