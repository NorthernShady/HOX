using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TraitsType
{
	MAX_HEALTH,
	ATTACK,
	DEFENCE,
	ATTACK_SPEED,
	MOVE_SPEED,
	CRITICAL_CHANCE,
	CRITICAL_MODIFIER,
	TRAITS_COUNT
}

[System.Serializable]
public class CommonTraits
{
	int m_exp = 0;
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

	public CommonTraits() {}

	public CommonTraits(CommonConfig config)
	{
		m_traits[(int)TraitsType.MAX_HEALTH] = config.HP;
		m_traits[(int)TraitsType.ATTACK] = config.Attack;
		m_traits[(int)TraitsType.DEFENCE] = config.Defence;
		m_traits[(int)TraitsType.ATTACK_SPEED] = config.AttackSpeed;
		m_traits[(int)TraitsType.MOVE_SPEED] = config.Speed;
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
