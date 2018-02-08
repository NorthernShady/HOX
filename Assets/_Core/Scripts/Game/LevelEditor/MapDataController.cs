using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataController : MonoBehaviour {

	[SerializeField]
	string m_mapDataName;

	[SerializeField]
	BasicGrid m_mapPrefab;

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
		foreach (var hero in heroes)
			mapData.heroesStartData[hero.team].positions.Add(hero.startPosition);
	}

	public void loadMapData()
	{
		clear();

		MapData mapData = Resources.Load<MapData>(m_mapDataName);

		var map = GameObject.Instantiate(m_mapPrefab, Vector3.zero, Quaternion.identity);
		map.createGrid(mapData.gridData);

		var creepPrefab = Resources.Load<Creep>(k.Resources.CREEP);
		foreach (var creepData in mapData.mapCreepData) {
			var creep = GameObject.Instantiate(creepPrefab, map.transform, false);
			creep.initialize(creepData);
		}

		var heroPrefab = Resources.Load<Hero>(k.Resources.HERO);
		for (var team = 0; team < mapData.heroesStartData.Length; ++team)
			foreach (var startPosition in mapData.heroesStartData[team].positions) {
				var hero = GameObject.Instantiate(heroPrefab, map.transform, false);
				hero.initialize(startPosition, team);
			}
	}

	void clear()
	{
		var mapGrid = FindObjectOfType<BasicGrid>();
		if (mapGrid != null)
			DestroyImmediate(mapGrid.gameObject);
	}
}
