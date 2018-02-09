using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPickController : MonoBehaviour {

	[SerializeField]
	List<CardButton> m_cards;

	[SerializeField]
	GameObject m_playButton;

	GameDataProxy m_gameDataProxy = null;

	void Awake()
	{
		m_gameDataProxy = FindObjectOfType<GameDataProxy>();
		m_playButton.SetActive(false);
	}

	void OnEnable()
	{
		m_cards.ForEach(x => x.OnCardToogled += onCardToogled);
	}

	void OnDisable()
	{
		m_cards.ForEach(x => x.OnCardToogled -= onCardToogled);
	}

	public void onCardToogled(GameData.HeroType type, bool active)
	{
		m_playButton.SetActive(active);

		if (active) {
			m_cards.ForEach(x => {
				if (x.type != type)
					x.setActive(false);
			});
			m_gameDataProxy.heroType = type;
		}
	}
}
