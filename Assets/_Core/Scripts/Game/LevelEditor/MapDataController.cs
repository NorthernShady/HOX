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
		foreach (var creep in creeps) {
			mapCreepData.Add(creep.creepData);
		}

		mapData.mapCreepData = mapCreepData;
	}

	public void loadMapData()
	{
		clear();

		MapData mapData = Resources.Load<MapData>(m_mapDataName);

		var map = GameObject.Instantiate(m_mapPrefab, Vector3.zero, Quaternion.identity);
//		map.GetComponent<BasicGrid>().createGrid(mapData.gridData);
		map.createGrid(mapData.gridData);

		var creepPrefab = Resources.Load<Creep>(k.Resources.CREEP);
		foreach (var creepData in mapData.mapCreepData) {
			var creep = GameObject.Instantiate(creepPrefab, map.transform, false);
			creep.initialize(creepData);
		}
	}

	void clear()
	{
		var mapGrid = FindObjectOfType<BasicGrid>();
		DestroyImmediate(mapGrid.gameObject);
	}
}
