using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPopup : BasePopup {

	[SerializeField]
	InventoryObserver m_inventory = null;

	[SerializeField]
	InventoryObserver m_enemyDrop = null;

	private Inventory m_heroInventory;

	private Inventory m_enemyInventory;

	public void initialize(Character hero, Character enemy)
	{
		m_heroInventory = hero.inventory;
		m_enemyInventory = enemy.inventory;

		m_inventory.initialize(m_heroInventory);
		m_enemyDrop.initialize(m_enemyInventory);
	}

	public void takeAll()
	{
		var drop = m_enemyInventory.items;
		if (m_heroInventory.freeSpace >= drop.Count)
			m_heroInventory.addItems(drop);

		onClose();
	}
}
