using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicController : MonoBehaviour
{
	void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

	public float countDamage(Character attacker, Character defender)
	{
		var attack = attacker.data + countInventory(attacker);
		var defence = defender.data + countInventory(defender);

		return Mathf.Max(attack.attack - defence.defence, 1.0f);
	}

	public CommonTraits countInventory(Character character)
	{
		var traits = new CommonTraits();

		if (character.getType() == GameData.CharacterType.HERO)
			foreach (var item in character.inventory.items)
				if (item != null)
					traits += item.data;

		return traits;
	}
}
