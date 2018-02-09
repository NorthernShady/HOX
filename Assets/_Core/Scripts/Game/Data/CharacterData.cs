using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {

	public float maxHealth;
	public float attack;
	public float defence;
	public float attackSpeed;
	public float moveSpeed;

	public CharacterData(CreepConfig config)
	{
		maxHealth = config.HP;
		attack = config.Attack;
		defence = config.Defence;
		attackSpeed = config.AttackSpeed;
		moveSpeed = config.Speed;
	}

	public CharacterData(HeroConfig config)
	{
		maxHealth = config.HP;
		attack = config.Attack;
		defence = config.Defence;
		attackSpeed = config.AttackSpeed;
		moveSpeed = config.Speed;
	}
}
