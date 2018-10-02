using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelSelector))]
public class LevelSelectorEditor : Editor
{
	string generatedText;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		GUI.enabled = true;

		LevelSelector t = target as LevelSelector;


		foreach (var scene in t.Scenes)
		{
			if (GUILayout.Button(scene.SceneName))
			{
				
			}
		}
	}
}

