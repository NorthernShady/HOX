using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAura : BasicSkill
{
	[SerializeField]
	float m_healAmount = 1.0f;

	[SerializeField]
	float m_healTime = 1.0f;

	List<Character> m_characters = new List<Character>();

	private void Start()
	{
		StartCoroutine(runAura());	
	}

	void OnTriggerEnter(Collider other)
	{
		var character = getCharacter(other);
		m_characters.Add(character);
	}

	void OnTriggerExit(Collider other)
	{
		var character = getCharacter(other);
		m_characters.Remove(character);
	}

	private Character getCharacter(Collider other)
	{
		var physicalModel = other.GetComponent<BasicPhysicalModel>();
		var targetObject = physicalModel.targetObject;
		return targetObject.GetComponent<Character>();
	}

	IEnumerator runAura()
	{
		var delay = new WaitForSeconds(m_healTime);

		while (true)
		{
			yield return delay;
			m_characters.ForEach(x => x.heal(m_healAmount, true));
		}
	}
}
