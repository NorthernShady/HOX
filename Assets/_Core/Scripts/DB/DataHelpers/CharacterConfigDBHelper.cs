using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterConfigDBHelper : MonoBehaviour {

	public static HeroConfig getHeroConfig(GameData.HeroType type, int level) {
		return DBProvider.instance<I_UserDBProvider>().getHeroConfig(type.ToString(), level);
	}

	public static CreepConfig getCreepConfig(GameData.CreepType type, int level) {
		return DBProvider.instance<I_UserDBProvider>().getCreepConfig(type.ToString(), level);
	}
}
