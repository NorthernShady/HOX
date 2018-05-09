using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapCreepData {
	public GameData.CreepType type = GameData.CreepType.NONE;
	public GameData.DomaineType domaine = GameData.DomaineType.NONE;
	public int level = 1;
	public Vector2 position = Vector2.zero;
}
