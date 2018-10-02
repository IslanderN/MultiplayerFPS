using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;

public class FlagInfo : EditorWindow
{

    [MenuItem("Testing/FlagInfo")]
    private static void ShowWindow()
    {
        GetWindow<FlagInfo>().Show();
    }

    private static Flag flag;

    private void OnGUI()
    {
        if (flag == null)
        {
            var maybeFlag = GameObject.FindObjectOfType<Flag>();

            if (maybeFlag == null)
                Debug.Log("Cant find flag");
            else
            {
                flag = maybeFlag;
            }
        }
        else
        {
            Rect rect = new Rect(0,0,50,50);
            GUI.Label(rect, flag.FlagOwner);
        }

    }

    void OnInspectorUpdate()
    {

        Repaint();
    }

}