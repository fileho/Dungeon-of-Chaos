using System;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Complex script that allows to set up sound effects in the editor
/// Each SFX has its category and then index in that category
/// This scripts shows proper SFX for currently selected categories
/// </summary>
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

        float width = position.width;

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        const int lineHeight = 20;

        // Calculate rects
        var categoryRect = new Rect(position.x, position.y, width, lineHeight);
        var soundRect = new Rect(position.x, position.y + lineHeight, width, lineHeight);
        var volumeRect = new Rect(position.x, position.y + lineHeight * 2, width, lineHeight);
        var pitchRect = new Rect(position.x, position.y + lineHeight * 3, width, lineHeight);

        var soundType = property.FindPropertyRelative("soundCategory");
        var soundIndex = property.FindPropertyRelative("sound");

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(categoryRect, soundType, GUIContent.none);

        // Show proper SFXs for given category
        var index = (SoundCategories.SoundCategory)soundType.enumValueIndex;
        switch (index)
        {
        case SoundCategories.SoundCategory.Looping: {
            SoundCategories.Looping s = (SoundCategories.Looping)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.Looping)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        case SoundCategories.SoundCategory.EnemyAmbients: {
            SoundCategories.EnemyAmbients s = (SoundCategories.EnemyAmbients)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.EnemyAmbients)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        case SoundCategories.SoundCategory.Attack: {
            SoundCategories.Attack s = (SoundCategories.Attack)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.Attack)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        case SoundCategories.SoundCategory.Skill: {
            SoundCategories.Skill s = (SoundCategories.Skill)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.Skill)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        case SoundCategories.SoundCategory.Ui: {
            SoundCategories.Ui s = (SoundCategories.Ui)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.Ui)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        case SoundCategories.SoundCategory.Items: {
            SoundCategories.Items s = (SoundCategories.Items)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.Items)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        case SoundCategories.SoundCategory.Death: {
            SoundCategories.Death s = (SoundCategories.Death)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.Death)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        case SoundCategories.SoundCategory.TakeDamage: {
            SoundCategories.TakeDamage s = (SoundCategories.TakeDamage)soundIndex.intValue;
            soundIndex.intValue = (int)(SoundCategories.TakeDamage)EditorGUI.EnumPopup(soundRect, s);
            break;
        }
        default:
            throw new ArgumentOutOfRangeException();
        }

        float labelWidth = EditorGUIUtility.labelWidth;
        EditorGUIUtility.labelWidth = 60;
        // Draw remaining fields
        EditorGUI.PropertyField(volumeRect, property.FindPropertyRelative("volume"));
        EditorGUI.PropertyField(pitchRect, property.FindPropertyRelative("pitch"));

        EditorGUIUtility.labelWidth = labelWidth;

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
