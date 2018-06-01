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
		BOOTS,
		BOW,
		POTION_HEAL
	}

	public enum ObstacleType
	{
		NONE,
		CANYON_1,
		CANYON_2,
		CANYON_3,
		CANYON_4,
		CANYON_5,
		TREE_1,
		TREE_2,
		TREE_3,
		TREE_4
	}
}

public static class StringExtensions
{
    public static TEnum ToEnum<TEnum> (this string stringEnumValue, TEnum defaultValue)
    {
        if (!System.Enum.IsDefined(typeof(TEnum), stringEnumValue))
        {
            return defaultValue;
        }

        return (TEnum)System.Enum.Parse(typeof(TEnum), stringEnumValue);
    }

    public static TEnum ToEnum<TEnum> (this string stringEnumValue)
    {
        return (TEnum)System.Enum.Parse(typeof(TEnum), stringEnumValue);
    }
}

public static class EnumExtensions
{
	public static string AsSprite(this GameData.HeroType enumValue)
	{
		switch (enumValue) {
			case GameData.HeroType.WARRIOR: return "icon_warrior";
			case GameData.HeroType.ROGUE: return "icon_rogue";
			case GameData.HeroType.MAGE: return "icon_wizard";
			default: return "icon_warrior";
		}
	}

	public static string AsSprite(this GameData.ItemType enumValue)
	{
		switch (enumValue) {
			case GameData.ItemType.AXE: return "ax";
			case GameData.ItemType.DAGGER: return "dagger";
			case GameData.ItemType.HAMMER: return "hammer";
			case GameData.ItemType.SPEAR: return "spear";
			case GameData.ItemType.SWORD: return "sword";
			case GameData.ItemType.SHIELD: return "shield";
			case GameData.ItemType.HELMET: return "helmet";
			case GameData.ItemType.CUIRASS: return "kirsa";
			case GameData.ItemType.PANTS: return "pants";
			case GameData.ItemType.BOOTS: return "boots";
			case GameData.ItemType.BOW: return "BOW";
			case GameData.ItemType.POTION_HEAL: return "POTION_HEAL";
			default: return "sword";
		}
	}
}

public static class EnumHelper
{
	public static TEnum Random<TEnum>(bool excludeFirst = true)
	{
		var values = System.Enum.GetValues(typeof(TEnum));
		var randomIndex = UnityEngine.Random.Range(excludeFirst ? 1 : 0, values.Length);
		return (TEnum)values.GetValue(randomIndex);
	}
}
