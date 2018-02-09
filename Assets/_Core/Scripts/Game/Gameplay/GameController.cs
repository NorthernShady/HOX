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
		StartCoroutine (addPlayer (m_gameDataProxy.team));
	}

	void initialize()
	{
		m_mapDataController.loadMapData(m_gameDataProxy.mapDataName);
	}

	IEnumerator addPlayer(int team = 0)
	{
		yield return new WaitForSeconds (3);
		Vector3 pos = Vector3.zero;
		if (team != 0) {
			pos = new Vector3 (5, 5, 0);
		}
//		var hero = System.Array.Find(FindObjectsOfType<Hero>(), x => x.team == team);
		var heroObj = PhotonNetwork.Instantiate (heroPrefab.name, pos, Quaternion.identity, 0);
	}
}
