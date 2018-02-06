using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDataController : MonoBehaviour {

	[SerializeField]
	string m_mapDataName;

	public string mapDataName
	{
		get {
			return m_mapDataName;
		}
	}

	public void saveMapData()
	{
	}

	public void loadMapData()
	{
	}
}
