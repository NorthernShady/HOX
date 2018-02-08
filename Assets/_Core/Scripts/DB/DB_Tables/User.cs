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
}
