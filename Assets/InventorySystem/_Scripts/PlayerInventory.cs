using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInventory : NetworkBehaviour {

	[SerializeField]
	private Inventory inventoryPrefab;

	private Inventory inventoryInstance;

	public Inventory InventoryInstance { get {return inventoryInstance;}}
	
	// Use this for initialization
	void Start () {
		if(isLocalPlayer)
		{
			inventoryInstance = Instantiate(inventoryPrefab);
			inventoryInstance.name = inventoryPrefab.name;
		}
	}
}
