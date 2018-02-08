using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataProxy : MonoBehaviour {

	public string mapDataName;
	public GameData.HeroType heroType;

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
