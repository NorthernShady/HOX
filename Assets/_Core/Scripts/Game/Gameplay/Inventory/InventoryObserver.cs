using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObserver : MonoBehaviour {

	[SerializeField]
	Tk2dInventoryVisual m_inventoryVisual = null;

	private Inventory m_inventory = null;

	public Inventory inventory {
		get {
			return m_inventory;
		}
	}

	void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

	void Disable()
	{
		m_inventory.OnItemsChanged -= onInventoryUpdated;
	}

	private void onInventoryUpdated(List<Item> items)
	{
		m_inventoryVisual.setItems(items);
	}

	public void initialize(Inventory inventory)
	{
		m_inventory = inventory;

		m_inventory.OnItemsChanged -= onInventoryUpdated;
		m_inventory.OnItemsChanged += onInventoryUpdated;

		onInventoryUpdated(m_inventory.items);
	}
}
