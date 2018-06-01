using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory {

	public System.Action<List<Item>> OnItemsChanged;

	private int m_maxItems = 6;
	public int maxItems {
		get {
			return m_maxItems;
		}
	}

	private List<Item> m_items = null;

	public List<Item> items {
		get {
			return m_items;
		}
	}

	public int numberOfItems {
		get {
			return m_items.Count;
		}
	}

	public int freeSpace {
		get {
			return m_maxItems - m_items.Count;
		}
	}

	public Inventory(int maxItems = 6)
	{
		m_maxItems = maxItems;
		m_items = new List<Item>(m_maxItems);
	}

	public Inventory(List<Item> items)
	{
		m_maxItems = items.Count;
		m_items = items;
	}

	public void consumeItem(Item item)
	{
		var itemIndex = m_items.FindIndex(x => x == item);
		setItem(itemIndex, null);
	}

	public void setItem(int index, Item item)
	{
		m_items[index] = item;
		broadcastUpdate();
	}

	public void setItems(List<Item> items)
	{
		m_items = new List<Item>();
		foreach (var item in items)
			if (item != null)
				m_items.Add(item);
		broadcastUpdate();
	}

	public void addItems(List<Item> items)
	{
		m_items.RemoveAll(x => x == null);
		m_items.AddRange(items);
		broadcastUpdate();
	}

    public void addItem(Item item)
    {
        m_items.RemoveAll(x => x == null);
        m_items.Add(item);
        broadcastUpdate();
    }

	private void broadcastUpdate()
	{
		if (OnItemsChanged != null)
			OnItemsChanged(m_items);
	}

    public void resetItems()
    {
        m_items.Clear();
        broadcastUpdate();
    }
}
