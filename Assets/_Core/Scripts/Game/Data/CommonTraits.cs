using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitsType
{
	MAX_HEALTH,
	MAX_HEALTH_PERCENT,
	ATTACK,
	ATTACK_PERCENT,
	DEFENCE,
	DEFENCE_PERCENT,
	ATTACK_SPEED,
	ATTACK_SPEED_PERCENT,
	MOVE_SPEED,
	MOVE_SPEED_PERCENT,
	CRITICAL_CHANCE,
	CRITICAL_CHANCE_PERCENT,
	CRITICAL_MODIFIER,
	TRAITS_COUNT
}

[System.Serializable]
public class CommonTraits
{
	int m_exp = 0;
	bool m_isConsumable = false;

	private float[] m_traits = new float[(int)TraitsType.TRAITS_COUNT];

	public float this[TraitsType type]
	{
		get {
			return m_traits[(int)type];
		}
		set {
			m_traits[(int)type] = value;
		}
	}

	public int exp {
		get {
			return m_exp;
		}
	}

	public float maxHealth {
		get {
			return m_traits[(int)TraitsType.MAX_HEALTH];
		}
	}

	public float attack {
		get {
			return m_traits[(int)TraitsType.ATTACK];
		}
	}

	public float defence {
		get {
			return m_traits[(int)TraitsType.DEFENCE];
		}
	}

	public float attackSpeed {
		get {
			return m_traits[(int)TraitsType.ATTACK_SPEED];
		}
	}

	public float moveSpeed {
		get {
			return m_traits[(int)TraitsType.MOVE_SPEED];
		}
	}

	public float criticalChance {
		get {
			return m_traits[(int)TraitsType.CRITICAL_CHANCE];
		}
	}

	public float criticalModifier {
		get {
			return m_traits[(int)TraitsType.CRITICAL_MODIFIER];
		}
	}

	public static CommonTraits create(GameData.HeroType type, int level)
	{
		return new CommonTraits(CharacterConfigDBHelper.getCharacterNorm(level),
								CharacterConfigDBHelper.getHeroConfig(type, level));
	}

	public static CommonTraits create(GameData.CreepType type, int level)
	{
		return new CommonTraits(CharacterConfigDBHelper.getCharacterNorm(level),
								CharacterConfigDBHelper.getCreepConfig(type, level));
	}

	public static CommonTraits create(GameData.ItemType type, int level = 1)
	{
		return new CommonTraits(CharacterConfigDBHelper.getItemConfig(type));
	}

	public CommonTraits() {}

	public CommonTraits(CommonConfig config)
	{
		m_exp = 0;
		fillCommonConfig(config);
	}

	public CommonTraits(ItemConfig config)
	{
		m_exp = 0;
		m_isConsumable = config.IsConsumable;
		fillCommonConfig(config);
	}

	public CommonTraits(CharacterNorm norm, CommonConfig config)
	{
		m_exp = norm.Exp;
		fillCommonConfig(config);
		
		m_traits[(int)TraitsType.MAX_HEALTH] += norm.HP;
		m_traits[(int)TraitsType.ATTACK] += norm.Attack;
		m_traits[(int)TraitsType.DEFENCE] += norm.Defence;
		m_traits[(int)TraitsType.ATTACK_SPEED] += norm.AttackSpeed;
		m_traits[(int)TraitsType.MOVE_SPEED] += norm.Speed;
		m_traits[(int)TraitsType.CRITICAL_CHANCE] += norm.CriticalChance;
		m_traits[(int)TraitsType.CRITICAL_MODIFIER] += norm.CriticalModifier;
	}

	public CommonTraits resolve()
	{
		var result = new CommonTraits();

		for (var i = 0; i < result.m_traits.Length; ++i)
			result.m_traits[i] = m_traits[i];

		m_traits[(int)TraitsType.MAX_HEALTH] *= 1.0f + m_traits[(int)TraitsType.MAX_HEALTH_PERCENT];
		m_traits[(int)TraitsType.ATTACK] *= 1.0f + m_traits[(int)TraitsType.ATTACK_PERCENT];
		m_traits[(int)TraitsType.DEFENCE] *= 1.0f + m_traits[(int)TraitsType.DEFENCE_PERCENT];
		m_traits[(int)TraitsType.ATTACK_SPEED] *= 1.0f + m_traits[(int)TraitsType.ATTACK_SPEED_PERCENT];
		m_traits[(int)TraitsType.MOVE_SPEED] *= 1.0f + m_traits[(int)TraitsType.MOVE_SPEED_PERCENT];
		m_traits[(int)TraitsType.CRITICAL_CHANCE] *= 1.0f + m_traits[(int)TraitsType.CRITICAL_CHANCE_PERCENT];

		return result;
	}

	private void fillCommonConfig(CommonConfig config)
	{
		m_traits[(int)TraitsType.MAX_HEALTH] = config.HP;
		m_traits[(int)TraitsType.MAX_HEALTH_PERCENT] = config.HpPercent;
		m_traits[(int)TraitsType.ATTACK] = config.Attack;
		m_traits[(int)TraitsType.ATTACK_PERCENT] = config.AttackPercent;
		m_traits[(int)TraitsType.DEFENCE] = config.Defence;
		m_traits[(int)TraitsType.DEFENCE_PERCENT] = config.DefencePercent;
		m_traits[(int)TraitsType.ATTACK_SPEED] = config.AttackSpeed;
		m_traits[(int)TraitsType.ATTACK_SPEED_PERCENT] = config.AttackSpeedPercent;
		m_traits[(int)TraitsType.MOVE_SPEED] = config.Speed;
		m_traits[(int)TraitsType.MOVE_SPEED_PERCENT] = config.SpeedPercent;
		m_traits[(int)TraitsType.CRITICAL_CHANCE] = config.CriticalChance;
		m_traits[(int)TraitsType.CRITICAL_CHANCE_PERCENT] = config.CriticalChancePercent;
		m_traits[(int)TraitsType.CRITICAL_MODIFIER] = config.CriticalModifier;
	}

	public static CommonTraits operator +(CommonTraits a, CommonTraits b)
	{
		var result = new CommonTraits();

		for (var i = 0; i < result.m_traits.Length; ++i)
			result.m_traits[i] = a.m_traits[i] + b.m_traits[i];

		return result;
	}

	public static CommonTraits operator *(CommonTraits a, CommonTraits b)
	{
		var result = new CommonTraits();

		for (var i = 0; i < result.m_traits.Length; ++i)
			result.m_traits[i] = a.m_traits[i] * b.m_traits[i];

		return result;
	}
}
