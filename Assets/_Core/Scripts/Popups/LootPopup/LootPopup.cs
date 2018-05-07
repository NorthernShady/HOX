using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPopup : BasePopup {

	[SerializeField]
	InventoryVisual m_inventory = null;

	[SerializeField]
	InventoryVisual m_enemyDrop = null;

	private Inventory m_heroInventory;

	private Inventory m_enemyInventory;

	public void initialize(Character hero, Character enemy)
	{
		m_heroInventory = hero.inventory;
		m_enemyInventory = enemy.inventory;

		m_inventory.setItems(m_heroInventory.items);
		m_enemyDrop.setItems(m_enemyInventory.items);
	}

	public void takeAll()
	{
		var drop = m_enemyInventory.items;
		if (m_heroInventory.freeSpace >= drop.Count)
			m_heroInventory.addItems(drop);

		onClose();
	}
}
