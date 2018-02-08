using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	[SerializeField]
	GameDataProxy m_fakeGameDataProxy = null;

	MapDataController m_mapDataController = null;
	GameDataProxy m_gameDataProxy = null;
	public GameObject heroPrefab;

	void Awake()
	{
		if (FindObjectOfType<AndroidBackButtonListener>() != null)
			DestroyImmediate(m_fakeGameDataProxy.gameObject);

		m_mapDataController = FindObjectOfType<MapDataController>();
		m_gameDataProxy = FindObjectOfType<GameDataProxy>();

		initialize();
		addPlayer(m_gameDataProxy.team);
	}

	void initialize()
	{
		m_mapDataController.loadMapData(m_gameDataProxy.mapDataName);
	}

	void addPlayer(int team = 0)
	{
		var hero = System.Array.Find(FindObjectsOfType<Hero>(), x => x.team == team);
//		Vector3 pos = Vector3.zero;
//		if (team != 0) {
//			pos = new Vector3 (5, 5, 0);
//		}
//		var heroObj = PhotonNetwork.Instantiate (heroPrefab.name, pos, Quaternion.identity, 0);
//		var hero = heroObj.GetComponent<Hero> ();
		hero.gameObject.AddComponent<Player>();
		hero.type = m_gameDataProxy.heroType;
	}
}
