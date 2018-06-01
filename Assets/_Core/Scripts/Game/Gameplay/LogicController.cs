using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicController : MonoBehaviour
{
	void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

	public System.Tuple<bool, float> countDamage(Character attacker, Character defender)
	{
		var attack = attacker.data + countInventory(attacker);
		var defence = defender.data + countInventory(defender);

		attack = attack.resolve();
		defence = defence.resolve();

		bool isCritical = 1.0f / attack.criticalChance < Random.Range(0.0f, 1.0f);
		var trueDamage = attack.attack * (isCritical ? attack.criticalModifier : 1.0f);
		var damage = Mathf.Max(trueDamage - defence.defence, 1.0f);

		return System.Tuple.Create(isCritical, damage);
	}

	public CommonTraits countInventory(Character character)
	{
		var traits = new CommonTraits();

		if (character.getType() == GameData.CharacterType.HERO)
			foreach (var item in character.inventory.items)
				if (item != null && !item.data.isConsumable)
					traits += item.data;

		return traits;
	}

	public GameData.DomaineType getDomaine(Hero hero)
	{	
		int[] domaineCount = new int[System.Enum.GetValues(typeof(GameData.DomaineType)).Length];

		foreach (var item in hero.inventory.items)
			if (item != null && !item.data.isConsumable)
				++domaineCount[(int)item.domaineType];

		var max = Mathf.Max(domaineCount);

		int numberOfValues = 0;
		int index = 0;
		for (var i = 0; i < domaineCount.Length; ++i) {
			if (domaineCount[i] == max) {
				++numberOfValues;
				index = i;
			}
		}

		if (numberOfValues > 1)
			return GameData.DomaineType.NONE;

		return (GameData.DomaineType)index;
	}
}
