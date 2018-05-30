using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class CharacterNorm
{
	[PrimaryKey, AutoIncrement]
	public int Level { get; set; }
	public int Exp { get; set; }
	public float HP { get; set; }
	public float Attack { get; set; }
	public float Defence { get; set; }
	public float Speed { get; set; }
	public float AttackSpeed { get; set; }
	public float CriticalChance { get; set; }
	public float CriticalModifier { get; set; }
	public float FightExp { get; set; }
}
