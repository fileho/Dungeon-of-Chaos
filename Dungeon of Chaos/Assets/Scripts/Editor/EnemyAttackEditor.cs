using UnityEditor;
using UnityEngine;

// Custom Editor using SerializedProperties.
[CustomEditor(typeof(EnemyAttack))]
public class EnemyAttackEditor : Editor
{
    SerializedProperty attack;
    SerializedProperty range;
    SerializedProperty cooldown;
    SerializedProperty delay;
    SerializedProperty attackCount;
    SerializedProperty delayBetweenAttacks;

    void OnEnable()
    {
        attack = serializedObject.FindProperty("attack");
        range = serializedObject.FindProperty("range");
        delay = serializedObject.FindProperty("delay");
        cooldown = serializedObject.FindProperty("cooldown");
        attackCount = serializedObject.FindProperty("attackCount");
        delayBetweenAttacks = serializedObject.FindProperty("delayBetweenAttacks");
    }

    public override void OnInspectorGUI()
    {
        // Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
        serializedObject.Update();

        EditorGUILayout.ObjectField(attack, new GUIContent("Attack"));

        // Show the custom GUI controls.
        EditorGUILayout.Slider(range, 1, 30, new GUIContent("Range"));
        EditorGUILayout.Slider(cooldown, 1, 20, new GUIContent("Cooldown", "When we can use this attack again?"));
        EditorGUILayout.Slider(delay, 0, 5, new GUIContent("Delay", "How long to wait after an attack?"));
        EditorGUILayout.IntSlider(attackCount, 1, 10, new GUIContent("AttackCount"));

        if (attackCount.intValue > 1)
            EditorGUILayout.Slider(delayBetweenAttacks, 0, 1, new GUIContent("Delay Between Attacks"));


        // Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
        serializedObject.ApplyModifiedProperties();
    }
}
