using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataProxy : MonoBehaviour {

	public string mapDataName;
	public GameData.HeroType heroType;
	public int team;
	public bool isBotGame;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
