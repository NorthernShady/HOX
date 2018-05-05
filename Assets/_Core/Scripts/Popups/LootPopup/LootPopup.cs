using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPopup : BasePopup {

	[SerializeField]
	InventoryObserver m_inventory = null;

	[SerializeField]
	InventoryObserver m_enemyDrop = null;

	Character m_hero = null;
	Character m_enemy = null;

	private Inventory m_heroInventory;

	private Inventory m_enemyInventory;

	private List<Item> m_drop;

	Inventory m_test = null;

	public void initialize(Character hero, Character enemy)
	{
		m_heroInventory = hero.inventory;
		m_enemyInventory = enemy.inventory;

		m_inventory.initialize(m_heroInventory);
		m_enemyDrop.initialize(m_enemyInventory);
		m_drop = new List<Item>(m_enemyInventory.items);

		Debug.Log("drop = " + (m_drop == null) + " " + (m_heroInventory == null) + " " + (m_test == null));
	}

	// public void takeAll()
	// {
	// 	Debug.Log("drop = " + (m_drop == null) + " " + (m_heroInventory == null) + " " + (m_test == null));
	// 	if (m_heroInventory.freeSpace >= m_drop.Count)
	// 		m_heroInventory.addItems(m_drop);

	// 	onClose();
	// }

	void smth()
	{
		Debug.Log("drop = " + (m_drop == null) + " " + (m_heroInventory == null) + " " + (m_test == null));
	}

	public void qwe()
	{
		smth();
		onClose();
	}
}
