using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[CustomEditor(typeof(ScenesList))]
public class ScenesListEditor : Editor
{
	string generatedText;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GUI.enabled = true;

		ScenesList t = target as ScenesList;

		if (t == null)
			return;

		if(t.Scenes.Count > 0)
		{
			if (GUILayout.Button("Show Window"))
			{
				SceneListWindow.ShowWindow();
			}
		}
			
	}
}