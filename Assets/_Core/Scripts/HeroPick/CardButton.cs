using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour {

	public System.Action<GameData.HeroType, bool> OnCardToogled;

	Dictionary<GameData.HeroType, string> iconNames = new Dictionary<GameData.HeroType, string>() {
		{ GameData.HeroType.WARRIOR, "card_warior" },
		{ GameData.HeroType.ROGUE, "card_rogue" },
		{ GameData.HeroType.MAGE, "card_wizard" },
	};

	[SerializeField]
	GameData.HeroType m_heroType;

	[SerializeField]
	tk2dSprite m_heroSign;

	[SerializeField]
	GameObject m_activation;

	void Awake()
	{
		m_heroSign.SetSprite(iconNames[m_heroType]);
	}

	public GameData.HeroType type {
		get {
			return m_heroType;
		}
	}

	public void setActive(bool active)
	{
		m_activation.SetActive(active);
	}

	public void toogle()
	{
		var active = !m_activation.activeInHierarchy;
		setActive(active);
		if (OnCardToogled != null)
			OnCardToogled(m_heroType, active);
	}
}
