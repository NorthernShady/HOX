using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tk2dItemObserver : MonoBehaviour {

	public System.Action<bool> OnItemSet;

	[SerializeField]
	GameObject m_gameObject = null;

	[SerializeField]
	tk2dSprite m_sprite = null;

	[SerializeField]
	tk2dTextMesh m_level = null;

	[SerializeField]
	ItemData m_itemData = null;
	
	Item m_item = null;

	public Item item {
		get {
			return m_item;
		}
	}

	virtual public void setItem(Item item)
	{
		bool hasItem = item != null && item.type != GameData.ItemType.NONE;

		m_item = item;
		m_gameObject.SetActive(hasItem);

		if (hasItem) {
			m_sprite.SetSprite(item.type.AsSprite());
			m_sprite.color = m_itemData.domaineColor[item.domaineType];

			if (m_level != null)
				m_level.text = item.data.level.ToString();
		}

		if (OnItemSet != null)
			OnItemSet(hasItem);
	}
}
