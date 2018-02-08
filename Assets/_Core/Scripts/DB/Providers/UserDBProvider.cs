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

	public CreepsConfig getCreepsConfig(string creepName, int level)
	{
		string cmdText = "SELECT * FROM Creeps WHERE Name = ? AND Level = ?";
		return dataService.connection.Query<CreepsConfig>(cmdText, creepName, level)[0];
	}

	public HeroesConfig getHeroesConfig(string heroName, int level)
	{
		string cmdText = "SELECT * FROM Creeps WHERE Name = ? AND Level = ?";
		return dataService.connection.Query<HeroesConfig>(cmdText, heroName, level)[0];
	}
}
