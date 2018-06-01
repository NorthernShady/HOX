using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConfigDBHelper {

	public static CharacterNorm getCharacterNorm(int level) {
		return DBProvider.instance<I_UserDBProvider>().getCharacterNorm(level);
	}

	public static CommonConfig getHeroConfig(GameData.HeroType type, int level) {
		return DBProvider.instance<I_UserDBProvider>().getHeroConfig(type.ToString(), level);
	}

	public static CommonConfig getCreepConfig(GameData.CreepType type, int level) {
		return DBProvider.instance<I_UserDBProvider>().getCreepConfig(type.ToString(), level);
	}

	public static ItemConfig getItemConfig(GameData.ItemType type) {
		return DBProvider.instance<I_UserDBProvider>().getItemConfig(type.ToString());
	}

	public static DomaineConfig getDomaineConfig(string configName) {
		return DBProvider.instance<I_UserDBProvider>().getDomaineConfig(configName);
	}
}
