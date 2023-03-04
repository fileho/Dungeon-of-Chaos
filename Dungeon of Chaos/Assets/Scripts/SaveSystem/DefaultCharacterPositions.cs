using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sets the default position for character for each level
/// </summary>
[CreateAssetMenu(menuName = "SO/Save/DefaultPositions")]
public class DefaultCharacterPositions : ScriptableObject
{
    public List<Vector3> positions;
}
