using System.Collections;
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
	public float HpPercent { get; set; }
	public float Attack { get; set; }
	public float AttackPercent { get; set; }
	public float Defence { get; set; }
	public float DefencePercent { get; set; }
	public float Speed { get; set; }
	public float SpeedPercent { get; set; }
	public float AttackSpeed { get; set; }
	public float AttackSpeedPercent { get; set; }
	public float CriticalChance { get; set; }
	public float CriticalChancePercent { get; set; }
	public float CriticalModifier { get; set; }
	public float FightExp { get; set; }
}

public class HeroConfig : CommonConfig {}
public class CreepConfig : CommonConfig {}

public class ItemConfig : CommonConfig
{
	public bool IsConsumable { get; set; }
}
