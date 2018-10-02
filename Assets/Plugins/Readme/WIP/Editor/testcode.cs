using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
namespace Readme
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ReadmeTest))]
    public class testcode : Editor
    {
        private SerializedProperty _property;
        private ReorderableList _list;

        bool SetUp = true;

        private void OnEnable()
        {
            _property = serializedObject.FindProperty("sections");
            _list = new ReorderableList(serializedObject, _property, true, true, true, true)
            {
                drawHeaderCallback = DrawListHeader,
                drawElementCallback = DrawListElement
            };
        }

        private void DrawListHeader(Rect rect)
        {
            GUI.Label(rect, _property.name);
        }

        private void DrawListElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var item = _property.GetArrayElementAtIndex(index);
            GUI.Label(rect, item.displayName);

        }

        public override void OnInspectorGUI()
        {

            if (SetUp)
            {
                base.OnInspectorGUI();
            }
            else if (!SetUp)
            {
                serializedObject.Update();
                EditorGUILayout.Space();
                _list.DoLayoutList();
                serializedObject.ApplyModifiedProperties();
            }



            // if (GUILayout.Button("SetUP"))
            // {
            //     SetUp = !SetUp;
            // }
        }
    }
#endif
}