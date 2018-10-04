using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataProxy : MonoBehaviour
{
	public string mapDataName;
	public GameData.HeroType heroType;
	public LightType lightType = LightType.Directional;
	public int team;
	public bool isBotGame;
	public bool hasWon;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
