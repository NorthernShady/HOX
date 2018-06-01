using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Game/Create MapData")]
public class MapData : ScriptableObject {

	[SerializeField]
	public GridData gridData;

	[SerializeField]
	public List<ObstacleData> obstacleData;

	[SerializeField]
	public List<MapCreepData> mapCreepData;

	[System.Serializable]
	public class HeroStartData
	{
		[SerializeField]
		public List<Vector2> positions = new List<Vector2>();
	}

	[SerializeField]
	public HeroStartData[] heroesStartData = new HeroStartData[2];
}
