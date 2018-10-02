using System;
using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;


namespace Readme
{
#if UNITY_EDITOR
    [CreateAssetMenu(fileName = "MyReadmeTest", menuName = "Readme/WIP/ReadmeTest")]
    public class ReadmeTest : ScriptableObject
    {

        [HideInInspector]
        public bool SetUpMode = true;
        [HideInInspector]
        public ReadmeDisplayStyle Style = ReadmeDisplayStyle.Inspector;

        public Texture2D icon;
        public string title;
        public Section[] sections;

        // public GameObject prefabForPreview;
        // public bool loadedLayout;

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

        //  [MenuItem("GameObject/MyMenu/Set Up", false, 0)]
        //  static void Init() {
        //      Debug.Log("here");
        //  }
    }
#endif
}