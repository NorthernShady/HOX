using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class DomaineConfig {
	
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
	public float Disadvantage { get; set; }
	public float Equal { get; set; }
	public float Advantage { get; set; }
}
