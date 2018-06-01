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

    protected virtual void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

    protected virtual void OnEnable()
	{
		var itemControllers = m_inventoryVisual.getItemControllers();

		foreach (var controller in itemControllers)
			controller.OnItemUsed += onItemUsed;
	}

    protected virtual void OnDisable()
	{
		m_character.inventory.OnItemsChanged -= onInventoryUpdated;

		var itemControllers = m_inventoryVisual.getItemControllers();

		foreach (var controller in itemControllers)
			controller.OnItemUsed += onItemUsed;
	}

    protected virtual void onInventoryUpdated(List<Item> items)
	{
		m_inventoryVisual.setItems(items);
	}

	public virtual void initialize(Character character)
	{
		m_character = character;

		var inventory = m_character.inventory;

		inventory.OnItemsChanged -= onInventoryUpdated;
		inventory.OnItemsChanged += onInventoryUpdated;

		onInventoryUpdated(inventory.items);
	}

    protected virtual void onItemUsed(Item item)
	{
		m_character.useItem(item);
	}
}
