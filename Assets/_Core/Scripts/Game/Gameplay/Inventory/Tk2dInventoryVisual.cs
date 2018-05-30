using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tk2dInventoryVisual : MonoBehaviour {

	[SerializeField]
	private List<GameObject> m_itemPlaces = null;

	public void setItems(List<Item> items)
	{
		var numberOfItems = Mathf.Min(m_itemPlaces.Count, items.Count);

		for (var i = 0; i < numberOfItems; ++i)
			setItem(m_itemPlaces[i], items[i]);

		for (var i = numberOfItems; i < m_itemPlaces.Count; ++i)
			setItem(m_itemPlaces[i], null);
	}

	public List<Item> getItems()
	{
		return m_itemPlaces.ConvertAll(x => getItem(x));
	}

	private void setItem(GameObject itemPlace, Item item)
	{
		var itemObserver = itemPlace.GetComponentInChildren<Tk2dItemObserver>(true);
		itemObserver.setItem(item);
	}

	private Item getItem(GameObject itemPlace)
	{
		return itemPlace.GetComponentInChildren<Tk2dItemObserver>().item;
	}
}
