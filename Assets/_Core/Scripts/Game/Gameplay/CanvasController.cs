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

    public void openLootPopup(Character hero, Character enemy, Player player)
	{
		var popup = GameObject.Instantiate(m_lootPopupPrefab, Vector3.zero, Quaternion.identity);
		popup.transform.position = new Vector3(0.0f, 0.0f, -5.0f);
		//popup.transform.SetParent(transform, false);

		popup.initialize(hero, enemy, player);
	}
}
