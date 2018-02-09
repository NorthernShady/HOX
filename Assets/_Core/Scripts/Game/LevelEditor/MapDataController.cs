using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDataController : MonoBehaviour {

	[SerializeField]
	string m_mapDataName;

	[SerializeField]
	BasicGrid m_mapPrefab;

	[SerializeField]
	Hero m_heroPrefab;

	[SerializeField]
	Creep m_creepPrefab;

	public string mapDataName
	{
		get {
			return m_mapDataName;
		}
	}

	public void saveMapData()
	{
		MapData mapData = Resources.Load<MapData>(m_mapDataName);

		var mapGrid = FindObjectOfType<BasicGrid>();
		mapData.gridData = mapGrid.gridData;

		var creeps = FindObjectsOfType<Creep>();
		var mapCreepData = new List<MapCreepData>();
		foreach (var creep in creeps)
			mapCreepData.Add(creep.creepData);

		mapData.mapCreepData = mapCreepData;

		var heroes = FindObjectsOfType<Hero>();
		var heroesStartData = new MapData.HeroStartData[2] { new MapData.HeroStartData(), new MapData.HeroStartData() };
		foreach (var hero in heroes)
			heroesStartData[hero.team].positions.Add(hero.startPosition);
		mapData.heroesStartData = heroesStartData;
	}

	public void loadMapData(string mapDataName)
	{
		m_mapDataName = mapDataName;
		loadMapData();
	}

	public void loadMapData()
	{
		clear();

		MapData mapData = Resources.Load<MapData>(m_mapDataName);

		var map = GameObject.Instantiate(m_mapPrefab, Vector3.zero, Quaternion.identity);
		map.createGrid(mapData.gridData);
		map.gameObject.isStatic = true;

		StartCoroutine (loadCharCoroutine (map.gameObject));

	}

	void clear()
	{
		var mapGrid = FindObjectOfType<BasicGrid>();
		if (mapGrid != null)
			DestroyImmediate(mapGrid.gameObject);
	}

	IEnumerator loadCharCoroutine(GameObject map)
	{
		yield return new WaitForSeconds (1.5f);
		var proxyData = FindObjectOfType<GameDataProxy> ();
		MapData mapData = Resources.Load<MapData> (m_mapDataName);
		for (var team = 0; team < mapData.heroesStartData.Length; ++team) {
			foreach (var startPosition in mapData.heroesStartData[team].positions) {
				if (proxyData.team != team) {
					continue;
				}
				var hero = PhotonNetwork.Instantiate (m_heroPrefab.name, startPosition, Quaternion.identity, 0);
				hero.GetComponent<Hero> ().initialize (startPosition, team);
			}
		}

		if (PhotonNetwork.isMasterClient) {
			foreach (var creepData in mapData.mapCreepData) {
				var creep = PhotonNetwork.Instantiate (m_creepPrefab.name, map.transform.position, Quaternion.identity, 0);
				creep.GetComponent<Creep>().initialize(creepData);
			}
		}

			
	}
}
