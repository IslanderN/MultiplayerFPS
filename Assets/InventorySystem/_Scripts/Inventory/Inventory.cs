using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton
	public static Inventory instance;

	void Awake()
	{
		if (instance != null) 
		{
			Debug.LogWarning ("More than one instance of Inventory found");
			return;
		}
		instance = this;
	}
	#endregion

	public delegate void OnItemChanged ();
	public OnItemChanged onItemChangedCallBack;

	public int inventorySize = 20; // Количество мест в интентаре

	public List<Item> items = new List<Item>();

	public bool Add (Item item)
	{
		if (!item.isDefaultItem) 
		{
			if (items.Count >= inventorySize)
			{
				Debug.Log ("Not enough room!");
				return false;
			}

			items.Add (item);	

			if(onItemChangedCallBack != null) // to do change ui
				onItemChangedCallBack.Invoke ();
		}
		return true;
	}

	public void Remove (Item item) 
	{
		//Instantiate(item, new Vector3(2.0f, 0.5f, -5.0f), Quaternion.identity);
		//ScriptableObject.CreateInstance("AK 47");

		items.Remove (item);

		if(onItemChangedCallBack != null)
			onItemChangedCallBack.Invoke ();
	}
}
