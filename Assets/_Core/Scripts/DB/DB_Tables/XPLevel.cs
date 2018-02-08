using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class XPLevel {

	[PrimaryKey]
	public int Id { get; set; }
	public int RequiredXP { get; set; }
}
