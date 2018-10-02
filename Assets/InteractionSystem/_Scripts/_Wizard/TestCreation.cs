using UnityEngine;
using UnityEditor;

public class TestCreation : EditorWindow
{

    [MenuItem("InteractionSystem/NotWorking/TestCreation")]
    private static void ShowWindow()
    {
        GetWindow<TestCreation>().Show();
    }

    private MonoScript logicVar; //= ScriptableObject.CreateInstance<Logic>();

    private MonoScript tagVar; //= ScriptableObject.CreateInstance<Logic>();



    private void OnGUI()
    {


        if (logicVar == null)
        {
            logicVar = EditorGUILayout.ObjectField(logicVar, typeof(MonoScript), false) as MonoScript;
            GUILayout.Button("tag null");
        }
        else
        {
            logicVar = EditorGUILayout.ObjectField(logicVar, typeof(MonoScript), false) as MonoScript;

            System.Type type = tagVar.GetClass();

            if (type == null)
            {
                GUILayout.Button("type null");
            }
            else
            {
                GUILayout.Button(type.ToString());

                if (type == typeof(Logic))
                {
                    GUILayout.Button("THis is logic type");
                }
            }
        }
    }
}