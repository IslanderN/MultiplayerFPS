using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MapToggle : MonoBehaviour {

	//[SerializeField]
	//public Scene Map;

	public SceneField Map;

	//public SceneAsset scene;

	//[SerializeField]
	//public string SceneName;

	public void SelectThisMap()
	{

		Debug.Log("NetworkManager.singleton.onlineScene  = " + NetworkManager.singleton.onlineScene);
		NetworkManager.singleton.onlineScene = Map.SceneName.Remove(0, 7);
		Debug.Log("NetworkManager.singleton.onlineScene  = " + NetworkManager.singleton.onlineScene);


	}


	public void UpdateSelectedMapTextField()
	{
		var mapSelector = FindObjectOfType<MapSelector>();

		if(mapSelector == null)
		{
			Debug.Log("Cant find map selector");
			return;
		}

		mapSelector.UpdateSelectedMapName();

	}
}
