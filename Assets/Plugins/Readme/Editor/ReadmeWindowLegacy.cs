using UnityEngine;
using UnityEditor;
using System;
namespace Readme
{

#if UNITY_EDITOR

    public class ReadmeWindowLegacy : EditorWindow
    {

        //[MenuItem("Readme/Readme help window")]
        private static void ShowWindow()
        {
            GetWindow<ReadmeWindowLegacy>().Show();
        }

        public static void ShowWindowForSelectedReadme(Readme rm)
        {
            selectedReadme = (Readme)rm;
            ShowWindow();
        }



        public static Readme selectedReadme;

        private void OnGUI()
        {
            if (selectedReadme != null)
            {
                Readme copy = UnityEngine.Object.Instantiate(selectedReadme) as Readme;
                copy.Style = ReadmeDisplayStyle.Window;
                var editor = Editor.CreateEditor(copy);
                editor.OnInspectorGUI();


                //GUILayout.TextField("!= null" );
            }
            else
            {
                GUILayout.TextField("== null");
            }
        }


        private void Update()
        {
            Repaint();
        }
    }
#endif
}