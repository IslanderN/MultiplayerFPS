using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace Readme
{
#if UNITY_EDITOR



    // IngredientDrawer
    [CustomPropertyDrawer(typeof(ReadmeTest.CheckMark))]
    public class IngredientDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var headingtRect = new Rect(position.x, position.y, 30, EditorGUIUtility.singleLineHeight);

            var height = 0f;
            height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            var textRect = new Rect(position.x + 35, position.y + height, position.width, EditorGUIUtility.singleLineHeight);
            var CheckMarksRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            //var value = (bool) property.FindPropertyRelative("Value") as bool;
            var text = property.FindPropertyRelative("Text");
            // EditorGUILayout.Toggle("",value);
            EditorGUI.Toggle(headingtRect, false);
            //EditorGUI.PropertyField(headingtRect, property.FindPropertyRelative("heading"), GUIContent.none);
            //EditorGUI.PropertyField(textRect, property.FindPropertyRelative("text"), GUIContent.none);
            EditorGUI.PropertyField(CheckMarksRect, property.FindPropertyRelative("Text"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
#endif
}