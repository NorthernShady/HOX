using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObserver : MonoBehaviour {

	public System.Action<Item> OnItemUsed;

	[SerializeField]
	Tk2dInventoryVisual m_inventoryVisual = null;

	private Character m_character = null;

	// public Inventory inventory {
	// 	get {
	// 		return m_inventory;
	// 	}
	// }

	void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

	void OnEnable()
	{
		var itemControllers = m_inventoryVisual.getItemControllers();

		foreach (var controller in itemControllers)
			controller.OnItemUsed += onItemUsed;
	}

	void OnDisable()
	{
		m_character.inventory.OnItemsChanged -= onInventoryUpdated;

		var itemControllers = m_inventoryVisual.getItemControllers();

		foreach (var controller in itemControllers)
			controller.OnItemUsed += onItemUsed;
	}

	private void onInventoryUpdated(List<Item> items)
	{
		m_inventoryVisual.setItems(items);
	}

	public void initialize(Character character)
	{
		m_character = character;

		var inventory = m_character.inventory;

		inventory.OnItemsChanged -= onInventoryUpdated;
		inventory.OnItemsChanged += onInventoryUpdated;

		onInventoryUpdated(inventory.items);
	}

	void onItemUsed(Item item)
	{
		m_character.useItem(item);
	}
}
