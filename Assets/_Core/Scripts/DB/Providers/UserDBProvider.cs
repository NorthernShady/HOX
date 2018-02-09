using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DBProvider : I_UserDBProvider  {

	public User getUser ()
	{
		return dataService.connection.Table<User> ().First();
	}

	public XPLevel getXPLevel (int levelId) {
		string cmdText = "SELECT * FROM XPLevel WHERE Id = ?";

		return dataService.connection.Query<XPLevel> (cmdText, levelId)[0];
	}

	public CreepConfig getCreepConfig(string creepName, int level)
	{
		string cmdText = "SELECT * FROM CreepConfig WHERE Name = ? AND Level = ?";
		return dataService.connection.Query<CreepConfig>(cmdText, creepName, level)[0];
	}

	public HeroConfig getHeroConfig(string heroName, int level)
	{
		string cmdText = "SELECT * FROM HeroConfig WHERE Name = ? AND Level = ?";
		return dataService.connection.Query<HeroConfig>(cmdText, heroName, level)[0];
	}
}
