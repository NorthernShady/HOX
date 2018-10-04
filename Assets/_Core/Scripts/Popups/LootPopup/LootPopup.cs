using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootPopup : BasePopup {

	[SerializeField]
    GameObject m_popup = null;

    [SerializeField]
    float m_openDelay = 1.0f;

	[SerializeField]
	Tk2dInventoryVisual m_inventory = null;

	[SerializeField]
	Tk2dInventoryVisual m_enemyDrop = null;

	[SerializeField]
	ButtonController m_takeAllButton = null;

	private Inventory m_heroInventory;

	private Inventory m_enemyInventory;

    Player m_player = null;

    public void initialize(Character hero, Character enemy, Player player)
	{
		m_heroInventory = hero.inventory;
		m_enemyInventory = enemy.inventory;
        m_player = player;


        m_inventory.setItems(m_heroInventory.items);
		m_enemyDrop.setItems(m_enemyInventory.items);

		bool enableTakeAllButton = m_heroInventory.freeSpace >= m_enemyInventory.items.Count;
		m_takeAllButton.isEnabled = enableTakeAllButton;
		// m_takeAllButton.GetComponent<UnityEngine.UI.Button>().enabled = enableTakeAllButton;
		// m_takeAllButton.GetComponent<UnityEngine.UI.Image>().color = enableTakeAllButton ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1.0f);

		StartCoroutine(openPopup(m_openDelay, m_popup));
	}

	public override void onClose()
	{
        m_player.setItems(m_inventory.GetComponentsInChildren<Tk2dItemObserver>(true).ToList().ConvertAll(x => x.item));
		base.onClose();
	}

	public void takeAll()
	{
        var newItems = m_heroInventory.items.ToList();
        newItems.AddRange(m_enemyInventory.items);
        m_player.setItems(newItems);
		base.onClose();
	}
}
