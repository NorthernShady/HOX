using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootPopup : BasePopup {

	[SerializeField]
	InventoryVisual m_inventory = null;

	[SerializeField]
	InventoryVisual m_enemyDrop = null;

	[SerializeField]
	GameObject m_takeAllButton = null;

	private Inventory m_heroInventory;

	private Inventory m_enemyInventory;

	public void initialize(Character hero, Character enemy)
	{
		m_heroInventory = hero.inventory;
		m_enemyInventory = enemy.inventory;

		m_inventory.setItems(m_heroInventory.items);
		m_enemyDrop.setItems(m_enemyInventory.items);

		m_takeAllButton.SetActive(m_heroInventory.freeSpace >= m_enemyInventory.items.Count);
	}

	public override void onClose()
	{
		m_heroInventory.setItems(m_inventory.GetComponentsInChildren<ItemObserver>(true).ToList().ConvertAll(x => x.item));
		base.onClose();
	}

	public void takeAll()
	{
		m_heroInventory.addItems(m_enemyInventory.items);
		base.onClose();
	}
}
