using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tk2dItemObserver))]
public class ItemController : MonoBehaviour {

	public System.Action<Item> OnItemUsed;

	[SerializeField]
	Collider m_collider = null;

	Tk2dItemObserver m_itemObserver = null;

	void Awake()
	{
		m_itemObserver = GetComponent<Tk2dItemObserver>();
	}

	void OnEnable()
	{
		m_itemObserver.OnItemSet += onItemSet;
	}

	void OnDisable()
	{
		m_itemObserver.OnItemSet -= onItemSet;
	}

	void onItemSet(bool hasItem)
	{
		m_collider.enabled = hasItem && m_itemObserver.item.data.isConsumable;
	}

	void onItemUsed()
	{
		if (OnItemUsed != null)
			OnItemUsed(m_itemObserver.item);
	}
}
