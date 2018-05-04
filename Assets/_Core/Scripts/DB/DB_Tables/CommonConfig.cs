﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class CommonConfig
{
	[PrimaryKey, AutoIncrement]
	public int Id { get; set; }
	public string Name { get; set; }
	public int Level { get; set; }
	public float HP { get; set; }
	public float Attack { get; set; }
	public float Defence { get; set; }
	public float Speed { get; set; }
	public float AttackSpeed { get; set; }
}

public class HeroConfig : CommonConfig {}
public class CreepConfig : CommonConfig {}
public class ItemConfig : CommonConfig {}