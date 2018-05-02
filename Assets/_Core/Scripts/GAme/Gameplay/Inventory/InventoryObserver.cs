using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObserver : MonoBehaviour {

	[SerializeField]
	private List<ItemObserver> m_items = null;

	private Inventory m_inventory = null;

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
		var numberOfItems = Mathf.Min(m_items.Count, items.Count);
		for (var i = 0; i < numberOfItems; ++i)
			m_items[i].setItem(items[i]);

		for (var i = numberOfItems; i < m_items.Count; ++i)
			m_items[i].setEmptyItem();
	}

	public void initialize(Inventory inventory)
	{
		m_inventory = inventory;

		m_inventory.OnItemsChanged -= onInventoryUpdated;
		m_inventory.OnItemsChanged += onInventoryUpdated;

		onInventoryUpdated(m_inventory.items);
	}
}
