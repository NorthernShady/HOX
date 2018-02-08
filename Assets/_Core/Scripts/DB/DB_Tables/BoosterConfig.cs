using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class BoosterConfig {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; } 	
	public int UnlockLevel { get; set; }
	public int PurchaseAmount { get; set; }
	public int Cost { get; set; }
}
