using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFeature : ActiveFeature
{
	[SerializeField]
	tk2dBaseSprite m_icon = null;

	protected Item m_item = null;
	protected Inventory m_inventory = null;

	protected void OnDisable()
	{
		m_inventory.OnItemsChanged -= onItemsChanged;
	}

	public override void initialize(Character character)
	{
		base.initialize(character);
		m_inventory = character.inventory;
		m_inventory.OnItemsChanged += onItemsChanged;
	}

	protected override void onFeatureActivated()
	{
		var item = m_item;
		m_item = null;
		m_character?.useItem(item);
	}

	private void onItemsChanged(List<Item> items)
	{
		m_item = items.Find(x => x != null && x.type == GameData.ItemType.POTION_HEAL);
		bool hasItem = m_item != null;
		m_icon.gameObject.SetActive(hasItem);
		enableTouches(hasItem);
	}
}
