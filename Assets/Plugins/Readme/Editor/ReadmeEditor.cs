using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Reflection;



namespace Readme
{
#if UNITY_EDITOR

    [CustomEditor(typeof(Readme))]
    [InitializeOnLoad]
    public class ReadmeEditor : Editor
    {

        static string kShowedReadmeSessionStateName = "ReadmeEditor.showedReadme";

        static float kSpace = 16f;

        public ReadmeDisplayStyle Style;

        static ReadmeEditor()
        {
            EditorApplication.delayCall += SelectReadmeAutomatically;
        }

        static void SelectReadmeAutomatically()
        {
            if (!SessionState.GetBool(kShowedReadmeSessionStateName, false))
            {
                var readme = SelectReadme();
                SessionState.SetBool(kShowedReadmeSessionStateName, true);

                if (readme && !readme.loadedLayout)
                {
                    LoadLayout();
                    readme.loadedLayout = true;
                }
            }
        }

        // no need right now to load layout, can put it in ReadmePluginSetup
        static void LoadLayout()
        {
            // var assembly = typeof(EditorApplication).Assembly;
            // var windowLayoutType = assembly.GetType("UnityEditor.WindowLayout", true);
            // var method = windowLayoutType.GetMethod("LoadWindowLayout", BindingFlags.Public | BindingFlags.Static);
            // method.Invoke(null, new object[] { Path.Combine(Application.dataPath, "TutorialInfo/Layout.wlt"), false });
        }

        [MenuItem("Tutorial/Show Tutorial Instructions")]
        static Readme SelectReadme()
        {
            var ids = AssetDatabase.FindAssets("Readme t:Readme l:ReadmeMain");
            if (ids.Length == 1)
            {
                var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));

                Selection.objects = new UnityEngine.Object[] { readmeObject };

                return (Readme)readmeObject;
            }
            else
            {
                Debug.Log("0 or more than 1 main readme ");
                return null;
            }
        }

        protected override void OnHeaderGUI()
        {
            var readme = (Readme)target;
            Init();

            var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);

            GUILayout.BeginHorizontal("In BigTitle");
            {
                GUILayout.Label(readme.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
                GUILayout.Label(readme.title, TitleStyle);
            }
            GUILayout.EndHorizontal();
        }

        public override void OnInspectorGUI()
        {
            var readme = (Readme)target;

            if (readme.Style == ReadmeDisplayStyle.Window)
            {
                ShowReadme();
            }
            else
            {
                if (readme.SetUpMode)
                {
                    base.OnInspectorGUI();

                    if (GUILayout.Button("Show help window"))
                    {
                        ReadmeWindow.ShowWindowForSelectedReadme(readme);
                    }

                    if (GUILayout.Button("Exit editing"))
                        readme.SetUp();
                }
                else if (readme.SetUpMode == false)
                {
                    ShowReadme();

                    if (GUILayout.Button("Edit"))
                        readme.SetUp();
                }




                GUILayout.Space(10);
            }

        }

        public void ShowReadme()
        {
            var readme = (Readme)target;
            Init();

            if (readme.sections != null)
            {
                foreach (var section in readme.sections)
                {

    #region DefineOrderVariables
                    bool referensesAtTop = false;

    #region Logic
                    if (section.assetReferences != null)
                        if (section.ShowRefsBeforeText)
                            referensesAtTop = true;

    #endregion

    #endregion

                    if (!string.IsNullOrEmpty(section.heading))
                    {
                        GUILayout.Label(section.heading, HeadingStyle);
                    }


                    if (referensesAtTop)
                    {
                        ShowRefs(section.assetReferences);
                        if (!string.IsNullOrEmpty(section.text))
                            GUILayout.Label(section.text, BodyStyle);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(section.text))
                            GUILayout.Label(section.text, BodyStyle);
                        ShowRefs(section.assetReferences);
                    }

                    if (section.CheckMarks != null)
                    {
                        foreach (var cm in section.CheckMarks)
                        {
                            cm.Value = GUILayout.Toggle(cm.Value, cm.Text);
                        }
                    }


                    if (!string.IsNullOrEmpty(section.linkText))
                    {
                        if (LinkLabel(new GUIContent(section.linkText)))
                        {
                            Application.OpenURL(section.url);
                        }
                    }
                    GUILayout.Space(kSpace);
                }
            }



            if (readme.prefabForPreview != null)
            {
                var pf = readme.prefabForPreview;

                var img = AssetPreview.GetAssetPreview(pf);

                //EditorGUI.DrawPreviewTexture(position, img);

                GUILayout.Label(img, GUILayout.Width(img.width), GUILayout.Height(img.height));
            }
            if (Event.current.type == EventType.MouseDown && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
            {
                Debug.Log("Pressing prefab");
            }
        }

        private void ShowRefs(List<Readme.AssetReference> refs)
        {

            foreach (var item in refs)
            {
                if (item.ObjRef != null)
                {
                    /*readme.someAsset = (GameObject)*/
                    if (item.Label == null || item.Label == "")
                    {
                        // noting
                        EditorGUILayout.ObjectField(item.ObjRef, typeof(Nullable), false);
                    }
                    else
                    {
                        EditorGUILayout.ObjectField(item.Label, item.ObjRef, typeof(Nullable), false);
                    }
                }

                // if (item.SORef)
                //     EditorGUILayout.ObjectField(item.SORef, typeof(Nullable), false);
                // if (item.MBRef)
                //     EditorGUILayout.ObjectField(item.MBRef, typeof(Nullable), false);
            }
        }




        bool m_Initialized;

        GUIStyle LinkStyle { get { return m_LinkStyle; } }
        [SerializeField] GUIStyle m_LinkStyle;

        GUIStyle TitleStyle { get { return m_TitleStyle; } }
        [SerializeField] GUIStyle m_TitleStyle;

        GUIStyle HeadingStyle { get { return m_HeadingStyle; } }
        [SerializeField] GUIStyle m_HeadingStyle;

        GUIStyle BodyStyle { get { return m_BodyStyle; } }
        [SerializeField] GUIStyle m_BodyStyle;

        void Init()
        {
            if (m_Initialized)
                return;
            m_BodyStyle = new GUIStyle(EditorStyles.label);
            m_BodyStyle.wordWrap = true;
            m_BodyStyle.fontSize = 14;

            m_TitleStyle = new GUIStyle(m_BodyStyle);
            m_TitleStyle.fontSize = 26;

            m_HeadingStyle = new GUIStyle(m_BodyStyle);
            m_HeadingStyle.fontSize = 18;

            m_LinkStyle = new GUIStyle(m_BodyStyle);
            m_LinkStyle.wordWrap = false;
            // Match selection color which works nicely for both light and dark skins
            m_LinkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
            m_LinkStyle.stretchWidth = false;

            m_Initialized = true;
        }

        bool LinkLabel(GUIContent label, params GUILayoutOption[] options)
        {
            var position = GUILayoutUtility.GetRect(label, LinkStyle, options);

            Handles.BeginGUI();
            Handles.color = LinkStyle.normal.textColor;
            Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
            Handles.color = Color.white;
            Handles.EndGUI();

            EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

            return GUI.Button(position, label, LinkStyle);
        }
    }

#endif
}