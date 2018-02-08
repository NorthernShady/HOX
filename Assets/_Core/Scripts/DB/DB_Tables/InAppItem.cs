using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class InAppItem {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; } 	
	public int RewardType { get; set; }
	public int RewardAmount { get; set; }
	public string GooglePlayId { get; set; }
	public float PriceUSD { get; set; }

}
