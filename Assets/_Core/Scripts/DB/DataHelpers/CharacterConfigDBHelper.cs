using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConfigDBHelper {

	public static CommonConfig getHeroConfig(GameData.HeroType type, int level) {
		return DBProvider.instance<I_UserDBProvider>().getHeroConfig(type.ToString(), level);
	}

	public static CommonConfig getCreepConfig(GameData.CreepType type, int level) {
		return DBProvider.instance<I_UserDBProvider>().getCreepConfig(type.ToString(), level);
	}
}
