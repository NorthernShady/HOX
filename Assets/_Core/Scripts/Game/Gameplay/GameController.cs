using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[SerializeField]
	GameDataProxy m_fakeGameDataProxy = null;

	[SerializeField]
	GameObject m_directionalLight = null;

	[SerializeField]
	GameObject m_shine = null;

	[SerializeField]
	GameObject m_gameOverPrefab;

	MapDataController m_mapDataController = null;
	GameDataProxy m_gameDataProxy = null;

	void Awake()
	{
		if (FindObjectOfType<AndroidBackButtonListener>() != null)
			DestroyImmediate(m_fakeGameDataProxy.gameObject);

		m_mapDataController = FindObjectOfType<MapDataController>();
		m_gameDataProxy = FindObjectOfType<GameDataProxy>();

		if (m_gameDataProxy.lightType != LightType.Directional) {
			m_directionalLight.SetActive(false);
			m_shine.SetActive(false);
		}

		FindObjectOfType<Services>().addService(this);
	}

	void Start()
	{
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
			pos = new Vector3 (10, 0, 10);
		}
//		var hero = System.Array.Find(FindObjectsOfType<Hero>(), x => x.team == team);
//		var heroObj = PhotonNetwork.Instantiate (heroPrefab.name, pos, Quaternion.identity, 0);
//		var hero = PhotonNetwork.Instantiate (heroPrefab.name, pos, Quaternion.identity, 0);
//		hero.GetComponent<Hero>().initialize(pos, team);
	}

	public void onPlayerDeath(Character character)
	{
		character.OnDeath -= onPlayerDeath;
		m_gameDataProxy.hasWon = character.getHero().team != m_gameDataProxy.team;

		FindObjectOfType<Services>().getService<PopupController>().openResultPopup();
	}
}

