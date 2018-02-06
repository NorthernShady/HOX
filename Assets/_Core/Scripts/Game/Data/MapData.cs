using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Game/Create MapData")]
public class MapData : ScriptableObject {

	[SerializeField]
	GridData gridData;

	[SerializeField]
	List<MapCreepData> mapCreepData;

	[System.Serializable]
	class HeroStartData
	{
		[SerializeField]
		public List<Vector2> positions;
	}

	[SerializeField]
	HeroStartData[] heroesStartData = new HeroStartData[2];
}
