using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable] class CreepVisual : TypedMap<GameData.CreepType, GameObject> {}

public class Creep : Character {

	[SerializeField]
	CreepVisual m_creepVisual = null;

	[SerializeField]
	MapCreepData m_creepData;
	Transform m_hero = null;

	void Awake()
	{
		m_hero = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public MapCreepData creepData {
		get {
			m_creepData.position = new Vector2(transform.position.x, transform.position.z);
			return m_creepData;
		}
	}

	GameObject m_activeVisual = null;

	public void initialize(MapCreepData creepData)
	{
		m_creepData = creepData;
		transform.position = new Vector3(m_creepData.position.x, 0.0f, m_creepData.position.y);
		initialize(new CharacterData(CharacterConfigDBHelper.getCreepConfig(m_creepData.type, m_creepData.level)));

		updateVisual();
	}

	public void updateVisual()
	{
		if (m_activeVisual != null)
			DestroyImmediate(m_activeVisual.gameObject);

		m_activeVisual = GameObject.Instantiate(m_creepVisual[m_creepData.type], transform, false);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == k.Tags.PLAYER) {
			m_attackTarget = other.GetComponent<Character>();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == m_attackTarget.gameObject) {
			m_attackTarget = null;
		}
	}

	void runAnimation()
	{
	}
}
