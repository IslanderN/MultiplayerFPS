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
    public class ReadmeWindow : EditorWindow
    {

        static float kSpace = 16f;
        Vector2 scrollPos = new Vector2();


        [MenuItem("Readme/HelpWindow")]
        private static void ShowWindow()
        {
            GetWindow<ReadmeWindow>().Show();
        }

        public static void ShowWindowForSelectedReadme(Readme rm)
        {
            selectedReadme = (Readme)rm;
            ShowWindow();
        }

        public static Readme selectedReadme;

        // private void OnGUI()
        // {
        //     if (selectedReadme != null)
        //     {
        //         Readme copy = UnityEngine.Object.Instantiate(selectedReadme) as Readme;
        //         copy.Style = ReadmeDisplayStyle.Window;
        //         var editor = Editor.CreateEditor(copy);
        //         editor.OnInspectorGUI();


        //         //GUILayout.TextField("!= null" );
        //     }
        //     else
        //     {
        //         GUILayout.TextField("== null");
        //     }
        // }
        private void OnGUI()
        {
            if (selectedReadme != null)
            {
                // Readme copy = UnityEngine.Object.Instantiate(selectedReadme) as Readme;
                // copy.Style = ReadmeDisplayStyle.Window;
                // var editor = Editor.CreateEditor(copy);
                // editor.OnInspectorGUI();

                Readme readme = selectedReadme;
                Init();


                if (readme.sections != null)
                {
                    EditorGUILayout.BeginVertical();
                    scrollPos = 
                     EditorGUILayout.BeginScrollView(scrollPos);
                    
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
                    EditorGUILayout.EndScrollView();
                    EditorGUILayout.EndHorizontal();
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
        }

        void OnInspectorUpdate()
        {
            Repaint();
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