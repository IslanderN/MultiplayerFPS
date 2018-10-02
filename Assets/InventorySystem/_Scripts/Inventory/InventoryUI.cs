using UnityEngine;

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;
	public GameObject inventoryUI;

	Inventory inventory;

	InventorySlot[] slots;

	[Space]
	
	[SerializeField]
	private KeyCode inventoryKey;

	public GameEvent OnInventoryOpen;

	public GameEvent OnInventoryClose;

	void Start () {
		inventory = Inventory.instance;	
		inventory.onItemChangedCallBack += UpdateUI;

		slots = itemsParent.GetComponentsInChildren<InventorySlot> ();

		inventoryUI.SetActive(false);
	}
	
	void Update () {
		if (Input.GetKeyDown(inventoryKey)) {
			

			if (inventoryUI.activeSelf == false) {
			
				OnInventoryOpen.Raise();
			} else { 
			
				OnInventoryClose.Raise();
			}

			inventoryUI.SetActive (!inventoryUI.activeSelf);
		}
	}

	void UpdateUI() {
		Debug.Log ("UPDATING UI");

		for (int i = 0; i < slots.Length; i++) {
			if (i < inventory.items.Count) {
				slots [i].AddItem (inventory.items [i]);
			} else {
				slots [i].ClearSlot ();
			}

		}
	}
}
