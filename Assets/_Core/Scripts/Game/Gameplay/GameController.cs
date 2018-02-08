using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField]
	GameDataProxy m_fakeGameDataProxy = null;

	MapDataController m_mapDataController = null;
	GameDataProxy m_gameDataProxy = null;

	void Awake()
	{
		if (FindObjectOfType<AndroidBackButtonListener>() != null)
			DestroyImmediate(m_fakeGameDataProxy.gameObject);

		m_mapDataController = FindObjectOfType<MapDataController>();
		m_gameDataProxy = FindObjectOfType<GameDataProxy>();

		initialize();
		addPlayer();
	}

	void initialize()
	{
		m_mapDataController.loadMapData(m_gameDataProxy.mapDataName);
	}

	void addPlayer(int team = 0)
	{
		var hero = System.Array.Find(FindObjectsOfType<Hero>(), x => x.team == team);
		hero.gameObject.AddComponent<Player>();
		hero.type = m_gameDataProxy.heroType;
	}
}
