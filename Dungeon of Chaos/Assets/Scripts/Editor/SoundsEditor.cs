using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SoundSettings))]
public class SoundsEditor : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 4.5f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        const int lineHeight = 20;

        // Calculate rects
        var categoryRect = new Rect(position.x, position.y, 150, lineHeight);
        var soundRect = new Rect(position.x, position.y + lineHeight, 150, lineHeight);
        var volumeRect = new Rect(position.x, position.y + lineHeight * 2, 150, lineHeight);
        var pitchRect = new Rect(position.x, position.y + lineHeight * 3, 150, lineHeight);


        var soundType = property.FindPropertyRelative("soundCategory");
        var soundIndex = property.FindPropertyRelative("sound");


        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(categoryRect, soundType, GUIContent.none);

        int index = soundType.enumValueIndex;
        switch (index)
        {
            case 0:
            {
                SoundCategories.Ambient s = (SoundCategories.Ambient) soundIndex.intValue;
                soundIndex.intValue = (int) (SoundCategories.Ambient) EditorGUI.EnumPopup(soundRect, s);
                break;
            }
            case 1:
            {
                SoundCategories.FootSteps s = (SoundCategories.FootSteps) soundIndex.intValue;
                soundIndex.intValue = (int) (SoundCategories.FootSteps) EditorGUI.EnumPopup(soundRect, s);
                break;
            }
            case 2:
            {
                SoundCategories.Attack s = (SoundCategories.Attack) soundIndex.intValue;
                soundIndex.intValue = (int) (SoundCategories.Attack) EditorGUI.EnumPopup(soundRect, s);
                break;
            }
            case 3:
            {
                SoundCategories.Skill s = (SoundCategories.Skill) soundIndex.intValue;
                soundIndex.intValue = (int) (SoundCategories.Skill) EditorGUI.EnumPopup(soundRect, s);
                break;
            }
            case 4:
            {
                SoundCategories.Ui s = (SoundCategories.Ui) soundIndex.intValue;
                soundIndex.intValue = (int) (SoundCategories.Ui) EditorGUI.EnumPopup(soundRect, s);
                break;
            }
            case 5:
            {
                SoundCategories.Items s = (SoundCategories.Items) soundIndex.intValue;
                soundIndex.intValue = (int) (SoundCategories.Items) EditorGUI.EnumPopup(soundRect, s);
                break;
            }

            default:
                break;
        }

        EditorGUI.Slider(volumeRect, property.FindPropertyRelative("volume"), 0f, 1f);
        EditorGUI.Slider(pitchRect, property.FindPropertyRelative("pitch"), 0f, 2f);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
