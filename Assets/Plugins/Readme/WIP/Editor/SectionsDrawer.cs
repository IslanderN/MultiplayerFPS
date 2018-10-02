using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
namespace Readme
{

    // // IngredientDrawer
    // [CustomPropertyDrawer(typeof(ReadmeTest.Section))]
    // public class IngredientDrawer : PropertyDrawer
    // {
    //     // Draw the property inside the given rect
    //     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //     {
    //         // Using BeginProperty / EndProperty on the parent property means that
    //         // prefab override logic works on the entire property.
    //         EditorGUI.BeginProperty(position, label, property);

    //         // Draw label
    //         //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

    //         // Don't make child fields be indented
    //         var indent = EditorGUI.indentLevel;
    //         EditorGUI.indentLevel = 0;

    //         // Calculate rects
    //         var headingtRect = new Rect(position.x, position.y, 30,EditorGUIUtility.singleLineHeight);

    //         var height = 0f;
    //         height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
    //         var textRect = new Rect(position.x + 35,position.y + height, position.width, EditorGUIUtility.singleLineHeight);
    //         var CheckMarksRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

    //         // Draw fields - passs GUIContent.none to each so they are drawn without labels
    //         EditorGUI.PropertyField(headingtRect, property.FindPropertyRelative("heading"), GUIContent.none);
    //         EditorGUI.PropertyField(textRect, property.FindPropertyRelative("text"), GUIContent.none);
    //         EditorGUI.PropertyField(CheckMarksRect, property.FindPropertyRelative("CheckMarks"), GUIContent.none);

    //         // Set indent back to what it was
    //         EditorGUI.indentLevel = indent;

    //         EditorGUI.EndProperty();
    //     }
    // }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ReadmeTest.Section))]
    public class SectionDrawer : PropertyDrawer
    {
        ReorderableList chapterList;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float height = 0;
            EditorGUI.BeginProperty(position, label, property);
            {
                Rect titleRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(titleRect, property.FindPropertyRelative("heading"));
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

                Rect chaptersRect = new Rect(position.x, position.y + height, position.width, EditorGUIUtility.singleLineHeight);
                if (chapterList == null)
                {
                    chapterList = BuildChaptersReorderableList(property.FindPropertyRelative("CheckMarks"));
                }
                chapterList.DoList(chaptersRect);
            }
            EditorGUI.EndProperty();
        }

        private ReorderableList BuildChaptersReorderableList(SerializedProperty property)
        {
            ReorderableList list = new ReorderableList(property.serializedObject, property, true, true, true, true);

            list.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, property.displayName);
            };
            list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                EditorGUI.PropertyField(rect, property.GetArrayElementAtIndex(index), true);
            };
            return list;
        }
    }

#endif
}