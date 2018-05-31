using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObserver : MonoBehaviour {

	[SerializeField]
	GameObject m_gameObject = null;

	[SerializeField]
	UnityEngine.UI.Image m_image = null;

	[SerializeField]
	ItemData m_itemData = null;
	
	Item m_item = null;

	public Item item {
		get {
			return m_item;
		}
	}

	void Awake()
	{
	}

	public void setItem(Item item)
	{
		bool hasItem = item != null && item.type != GameData.ItemType.NONE;

		m_item = item;
		m_gameObject.SetActive(hasItem);

		if (hasItem) {
			m_image.sprite = m_itemData.itemImage[item.type];
			m_image.color = m_itemData.domaineColor[item.domaineType];
		}
	}
}
