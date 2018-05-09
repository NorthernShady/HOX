using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
	public enum DomaineType
	{
		NONE,
		RED,
		GREEN,
		BLUE,
	}

	public enum CharacterType
	{
		NONE,
		CREEP,
		HERO
	}

	public enum HeroType
	{
		NONE,
		WARRIOR,
		ROGUE,
		MAGE
	}

	public enum CreepType
	{
		NONE,
		SKELETON,
		PEASANT,
		SPIDER,
		DRAGON
	}

	public enum ItemType
	{
		NONE,
		AXE,
		DAGGER,
		HAMMER,
		SPEAR,
		SWORD,
		SHIELD,
		HELMET,
		CUIRASS,
		PANTS,
		BOOTS
	}

	public static class EnumExtensions
	{
		//public static ItemType Random(this ItemType enum)
	}
}
