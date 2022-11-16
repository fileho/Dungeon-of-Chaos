using UnityEngine;

[CreateAssetMenu(menuName = "SO/Indicator/Stomp Indicator Configuration")]
public class StompIndicatorConfiguration : MeleeIndicatorConfiguration {

    [Tooltip("Color of the secondary attack sprite")]
    public Color secondarySpriteColor = Color.white;

    [Tooltip("Scale of the secondary attack radius")]
    public float secondarySpriteScale = 1;
}