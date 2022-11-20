using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Save/DefaultPositions")]
public class DefaultCharacterPositions : ScriptableObject
{
    public List<Vector3> positions;
}
