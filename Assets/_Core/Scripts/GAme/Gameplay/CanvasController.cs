using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

	[SerializeField]
	LootPopup m_lootPopupPrefab = null;

	void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

	public void openLootPopup(Character hero, Character enemy)
	{
		var popup = GameObject.Instantiate(m_lootPopupPrefab, Vector3.zero, Quaternion.identity);
		popup.transform.SetParent(transform, false);

		m_lootPopupPrefab.initialize(hero, enemy);
	}
}
