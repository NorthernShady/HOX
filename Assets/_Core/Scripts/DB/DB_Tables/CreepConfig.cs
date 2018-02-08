using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class CreepConfig {

	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
	public int Level { get; set; }
	public int HP { get; set; }
	public int Attack { get; set; }
	public int Defence { get; set; }
}