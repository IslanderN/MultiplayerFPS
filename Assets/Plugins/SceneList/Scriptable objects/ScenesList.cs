using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Utilities;


[CreateAssetMenu(fileName = "NewScenesList", menuName = "ScenesList", order = 999)]
public class ScenesList : ScriptableObject
{
	public List<SceneField> Scenes;

	private string assetLabel = "SceneList";

#if UNITY_EDITOR
	private void OnEnable()
	{
		string[] labels = { assetLabel };

		AssetDatabase.SetLabels(this, labels);
	}
#endif
}
