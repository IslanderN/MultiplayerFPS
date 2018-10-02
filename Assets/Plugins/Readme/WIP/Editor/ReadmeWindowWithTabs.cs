using UnityEngine;
using UnityEditor;
using System;

namespace Readme
{
#if UNITY_EDITOR
    public class ReadmeWindowWithTabs : EditorWindow
    {

        [MenuItem("Readme/WIP/ReadmeWindowWithTabs")]
        private static void ShowWindow()
        {
            GetWindow<ReadmeWindowWithTabs>().Show();
        }



        // public enum Tabs
        // {
        //     ReadmeFiles, View
        // }


        // static Tabs tab = Tabs.View;

        // int toInt = (int)tab;

        int tab;


        private void OnGUI()
        {

            tab = GUILayout.Toolbar(tab, new string[] { "ReadmeFiles", "View" });
            switch (tab)
            {
                case 0:
                    ShowReadmeFiles();
                    break;
                case 1:
                    GUILayout.Button("V");
                    break;
                default:
                    Debug.Log("NOting");
                    break;
            }
        }


        void ShowReadmeFiles()
        {
            var guids = AssetDatabase.FindAssets("l:Readme", null);

            if (guids == null)
            {
                Debug.Log("Cant find things with this label");
            }
            else
            {
                foreach (var guid in guids)
                {
                    if (guid != null)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(guid);
                        var readme = AssetDatabase.LoadAssetAtPath(path, typeof(Readme)) as Readme;

                        EditorGUILayout.ObjectField(readme, typeof(Readme), false);
                    }
                }

            }
        }
    }
#endif

}