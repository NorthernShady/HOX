using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObserver : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Image m_image = null;

	[SerializeField]
	ItemData m_itemData = null;

	void Awake()
	{
		setEmptyItem();
	}

	public void setItem(Item item)
	{
		m_image.sprite = m_itemData.itemImage[item.type];
		m_image.color = m_itemData.domaineColor[item.domaineType];
	}

	public void setEmptyItem()
	{
		m_image.sprite = m_itemData.itemImage[GameData.ItemType.NONE];
		m_image.color = m_itemData.domaineColor[GameData.DomaineType.NONE];
	}
}
