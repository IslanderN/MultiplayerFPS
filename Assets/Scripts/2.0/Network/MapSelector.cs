using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Utilities;
using System.Linq;

[RequireComponent(typeof(ToggleGroup))]
public class MapSelector : MonoBehaviour {

	

	[Header("First Map will be selected by Default")]

	public List<SceneField> Maps;

	public GameObject Teamplate;

	public Text EditorInfo;

	public Text NextMapTextField;

	private ToggleGroup toggleGroup;

	private NetworkManager sceneNetworkManger;

	public void UpdateSelectedMapName()
	{
		string nextMapName = "SelectedMap is " + sceneNetworkManger.onlineScene;
		NextMapTextField.enabled = true;
		NextMapTextField.text = nextMapName;
	}
	private void Start()
	{
		sceneNetworkManger = NetworkManager.singleton;
		toggleGroup = GetComponent<ToggleGroup>();
		DisplayAllMaps();
		SetNewScene(Maps[0]);
		UpdateSelectedMapName();
	}

	private void DisplayAllMaps()
	{
		EditorInfo.gameObject.SetActive(false); // remove editor tip


		foreach (var map in Maps)
		{
			var toggle = Instantiate(Teamplate,this.transform);
			toggle.GetComponent<MapToggle>().Map = map;
			toggle.GetComponent<Toggle>().group = toggleGroup;

			if (Maps.IndexOf(map) == 0)
			{
				toggle.GetComponent<Toggle>().isOn = true;
				toggle.GetComponent<MapToggle>().SelectThisMap();
			}
				
			else
				toggle.GetComponent<Toggle>().isOn = false;

			toggle.transform.GetChild(1).GetComponent<Text>().text = map.SceneName;
			toggle.SetActive(true);
		}

		Teamplate.gameObject.SetActive(false);
	}

	public void SetNewScene(SceneField scene)
	{
		sceneNetworkManger.onlineScene = scene.SceneName.Remove(0, 7); ;

		UpdateSelectedMapName();
	}

}
