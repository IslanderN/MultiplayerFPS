using System;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;


namespace Readme
{
#if UNITY_EDITOR
    public enum ReadmeDisplayStyle
    {
        Inspector, Window
    }

    [CreateAssetMenu(fileName = "MyReadme", menuName = "Readme/Readme")]
    public class Readme : ScriptableObject
    {

        [HideInInspector]
        public bool SetUpMode = true;
        [HideInInspector]
        public ReadmeDisplayStyle Style = ReadmeDisplayStyle.Inspector;

        public Texture2D icon;
        public string title;
        public Section[] sections;

        public GameObject prefabForPreview;
        public bool loadedLayout;

        public bool IsMainReadmeFile = false;

        [Serializable]
        public class Section
        {

            public string heading;
            [TextArea]
            public string text;
            public List<CheckMark> CheckMarks;
            public List<AssetReference> assetReferences;
            public bool ShowRefsBeforeText = false;
            public string linkText;
            public string url;
        }

        [System.Serializable]
        public class AssetReference
        {
            public UnityEngine.Object ObjRef;

            // public ScriptableObject SORef;

            // public MonoBehaviour MBRef;
            public string Label = "";

        }
        [System.Serializable]
        public class CheckMark
        {
            public bool Value;
            public string Text;
        }


        /// Add a context menu named "Do Something" in the inspector
        /// of the attached script.
        [ContextMenu("SetUp")]
        public void SetUp()
        {
            SetUpMode = !SetUpMode;
        }

        // [ContextMenuItem] 
        // что то там

        [MenuItem("CONTEXT/SetUpMode", true, 2)]
        private static void NewMenuOption(MenuCommand menuCommand)
        {
            // The RigidBody component can be extracted from the menu command using the context field.
            var rm = (Readme)menuCommand.context as Readme;

            rm.SetUpMode = !rm.SetUpMode;
        }

        private const string assetLabel = "Readme";
        private const string mainAssetLabel = "ReadmeMain";

        private void OnEnable()
        {
            List<string> labels = new List<string>();

            if(IsMainReadmeFile) 
                labels.Add(mainAssetLabel);
            labels.Add(assetLabel);

            AssetDatabase.SetLabels(this, labels.ToArray());

            var settings = FindSettings();

            if (settings != null)
            {
                if (settings.ExitEditModeAutomatically)
                    SetUpMode = false;
            }

            else
            {
                Debug.Log("settings file not found i guess");
            }


        }

        private ReadmeSettings FindSettings()
        {
            var guids = AssetDatabase.FindAssets("l:ReadmeSettings", null);

            if (guids == null) {Debug.Log("Cant find things with this label"); return null;}
            else
            {
                if (guids.Length == 0) {Debug.LogWarning("Something wrong"); return null;}
                else if (guids.Length > 1) {Debug.LogWarning("More then one settings file"); return null;}
                else if (guids[0] != null)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    var readmeSettings = AssetDatabase.LoadAssetAtPath(path, typeof(ReadmeSettings)) as ReadmeSettings;

                    if (readmeSettings != null)
                    {
                        return readmeSettings;
                    }
                    else return null;
                }
                else return null;
            }
        }

        //  [MenuItem("GameObject/MyMenu/Set Up", false, 0)]
        //  static void Init() {
        //      Debug.Log("here");
        //  }
    }
#endif
}