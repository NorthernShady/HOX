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

	public CharacterNorm getCharacterNorm(int level)
	{
		string cmdText = "SELECT * FROM CharacterNorm WHERE Level = ?";
		return dataService.connection.Query<CharacterNorm>(cmdText, level)[0];
	}

	public CommonConfig getCreepConfig(string creepName, int level)
	{
		string cmdText = "SELECT * FROM CreepConfig WHERE Name = ? AND Level = ?";
		return dataService.connection.Query<CreepConfig>(cmdText, creepName, level)[0];
	}

	public CommonConfig getHeroConfig(string heroName, int level)
	{
		string cmdText = "SELECT * FROM HeroConfig WHERE Name = ? AND Level = ?";
		return dataService.connection.Query<HeroConfig>(cmdText, heroName, level)[0];
	}

	public ItemConfig getItemConfig(string itemName)
	{
		string cmdText = "SELECT * FROM ItemConfig WHERE Name = ?";
		return dataService.connection.Query<ItemConfig>(cmdText, itemName)[0];
	}

	public DomaineConfig getDomaineConfig(string configName)
	{
		string cmdText = "SELECT * FROM DomaineConfig WHERE Name = ?";
		return dataService.connection.Query<DomaineConfig>(cmdText, configName)[0];
	}
}
