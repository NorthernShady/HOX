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
	Player m_playerPrefab = null;

	[SerializeField]
	Hero m_heroPrefab;

	[SerializeField]
	Creep m_creepPrefab;

	[SerializeField]
	Obstacle m_obstaclePrefab = null;

	GameDataProxy m_dataProxy = null;

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

		var obstacles = FindObjectsOfType<Obstacle>();
		var obstaclesData = new List<ObstacleData>();
		foreach (var obstacle in obstacles)
			obstaclesData.Add(obstacle.obstacleData);

		mapData.obstacleData = obstaclesData;

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

		foreach (var obstacleData in mapData.obstacleData) {
			var obstacle = GameObject.Instantiate(m_obstaclePrefab, Vector3.zero, Quaternion.identity);
			obstacle.initialize(obstacleData);
			obstacle.transform.SetParent(map.transform, true);
		}

		m_dataProxy = FindObjectOfType<GameDataProxy>();

		if (!Application.isPlaying)
			loadCharactersEditor(mapData, map.gameObject);
		else
			StartCoroutine(loadCharCoroutine(mapData, map.gameObject));
	}

	void clear()
	{
		var mapGrid = FindObjectOfType<BasicGrid>();
		if (mapGrid != null)
			DestroyImmediate(mapGrid.gameObject);
	}

	IEnumerator loadCharCoroutine(MapData mapData, GameObject map)
	{
		if (!m_dataProxy.isBotGame)
			yield return new WaitForSeconds (1.5f);
		loadCharacters(mapData, map);
	}

	void loadCharacters(MapData mapData, GameObject map)
	{
		if (!PhotonHelper.isMaster())
			return;

		for (var team = 0; team < mapData.heroesStartData.Length; ++team) {
			foreach (var startPosition in mapData.heroesStartData[team].positions) {
				// if (!m_dataProxy.isBotGame && m_dataProxy.team != team) {
				// 	continue;
				// }
                var hero = PhotonHelper.Instantiate(m_heroPrefab, startPosition, Quaternion.identity, 0);
				var heroType = (m_dataProxy.team == team) ? m_dataProxy.heroType : getRandomHeroType();
				hero.GetComponent<Hero>().initialize(startPosition, team, heroType, m_dataProxy.team == team);
				hero.transform.SetParent(map.transform, true);
			}
		}

		foreach (var creepData in mapData.mapCreepData) {
			var creep = PhotonHelper.Instantiate (m_creepPrefab, map.transform.position, Quaternion.identity, 0);
			creep.GetComponent<Creep>().initialize(creepData);
			creep.transform.SetParent(map.transform, true);
		}
	}

	void loadCharactersEditor(MapData mapData, GameObject map)
	{
		for (var team = 0; team < mapData.heroesStartData.Length; ++team) {
			foreach (var startPosition in mapData.heroesStartData[team].positions) {
				var hero = PhotonHelper.Instantiate(m_heroPrefab, startPosition, Quaternion.identity, 0);
				var heroType = getRandomHeroType();
				hero.GetComponent<Hero>().initialize(startPosition, team, heroType, true);
				hero.transform.SetParent(map.transform, true);
			}
		}

		foreach (var creepData in mapData.mapCreepData) {
			var creep = PhotonHelper.Instantiate (m_creepPrefab, map.transform.position, Quaternion.identity, 0);
			creep.GetComponent<Creep>().initialize(creepData);
			creep.transform.SetParent(map.transform, true);
		}
	}

	GameData.HeroType getRandomHeroType()
	{
		var values = System.Enum.GetValues(typeof(GameData.HeroType));
		return (GameData.HeroType)values.GetValue(Random.Range(1, values.Length));
	}
}
