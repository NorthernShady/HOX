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
