using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class User {

	[PrimaryKey, AutoIncrement]
	public int IdKey { get; set; }
	public int Id { get; set; }
	public string Name { get; set; }
	public int XP { get; set; }
	public int CurrentLevel { get; set; }
	public int SoftCurrency { get; set; }
	public int HardCurrency { get; set; }
	public int ShuffleBoosterCount { get; set; }
	public int HammerBoosterCount { get; set; }
	public int RocketBoosterCount { get; set; }
	public int Lives { get; set; }
	public int MaxLives { get; set; }
	public long LifeRestorationTime { get; set; }
	public long LastLifeUpdateTime { get; set; }
	public bool NewMechanicsShown { get; set; }
	public bool TutorialInGameBoostersGiven { get; set; }
	public bool LastLevelCompleted { get; set; }
}
