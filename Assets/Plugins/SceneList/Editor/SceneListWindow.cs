using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using Utilities;
using UnityEngine.SceneManagement;
using System.Linq;

public class SceneListWindow : EditorWindow
{
	public static string text;

	public static ScenesList scenesList;

	public static int SlideIndex = 1;

	bool windowEmpty = true;

	// Add a menu item named "Do Something with a Shortcut Key" to MyMenu in the menu bar
	// and give it a shortcut (ctrl-g on Windows, cmd-g on macOS).
	[MenuItem("Tools/Next Scene %.")]
	static void NextScene()
	{

		if (SlideIndex >= scenesList.Scenes.Count)
		{
			//nothing
		}
		else
			++SlideIndex;


		var scene = scenesList.Scenes[SlideIndex - 1];
		if (scene != null)
		{
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			EditorSceneManager.OpenScene("Assets/" + scene + ".unity");
			//SceneManager.LoadScene("Assets/" + scene + ".unity");

		}
	}

	[MenuItem("Tools/Last Scene %,")]
	static void LastScene()
	{
		if (SlideIndex == 1)
		{
			//nothing
		}

		else
			--SlideIndex;


		var scene = scenesList.Scenes[SlideIndex - 1];
		if (scene != null)
		{
			EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
			EditorSceneManager.OpenScene("Assets/" + scene + ".unity");
		}
	}

	private static Scene CurrentScene()
	{
		return EditorSceneManager.GetActiveScene();
	}





	[MenuItem("Tools/Scene List")]
	public static void ShowWindow()
	{
		EditorWindow.GetWindow(typeof(SceneListWindow));
	}

	private void Awake()
	{


		//if (!scenesList)
		//{
		//	var allLists = Resources.FindObjectsOfTypeAll<ScenesList>();


		//	if (allLists.Length == 0) Debug.Log("Error here");
		//	else
		//	{
		//		scenesList = allLists[0];
		//	}
		//}
		//else
		//{
		//	SceneListWindow.ShowWindow();
		//}
	}

	//private void Update()
	//{
	//	//if(scenesList == null)
	//	//{
	//	//	Repaint();
	//	//	Debug.Log("Reapainting");
	//	//}
	//}


	void OnGUI()
	{

		if (scenesList == null)
		{

			//scenesList = EditorGUILayout.ObjectField(scenesList, typeof(ScenesList)) as ScenesList;

			///////////////////
			//var allLists = Resources.FindObjectsOfTypeAll<ScenesList>();

			//if (allLists.Length == 0) Debug.Log("Error here");
			//else
			//{
			//	scenesList = allLists[0];
			//}

			var guids = AssetDatabase.FindAssets("l:SceneList", null);

			if (guids == null)
			{
				Debug.Log("Cant find things with this label");
			}
			else
			{
				if (guids[0] != null)
				{
					string path = AssetDatabase.GUIDToAssetPath(guids[0]);
					scenesList = AssetDatabase.LoadAssetAtPath(path, typeof(ScenesList)) as ScenesList;
				}
			}
		}

		if (scenesList.Scenes != null && scenesList.Scenes.Count > 0)
			foreach (var scene in scenesList.Scenes)
			{
				if (scene != null)
					if (GUILayout.Button(scene))
					{
						EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
						EditorSceneManager.OpenScene("Assets/" + scene + ".unity");
					}

			}
	}
}
