using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObstacleData {
	
	public GameData.ObstacleType type = GameData.ObstacleType.NONE;
	public Vector3 position = Vector3.zero;
}
