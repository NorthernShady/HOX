using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

// I wanted to call field "Data" as "Value", but by some reason 
// sheet loader rejected this name. So, called "Data" for consistancy.

public class Config {
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public int ConfigId { get; set; }
	public string Name { get; set; }
	public string Data { get; set; } 
}
