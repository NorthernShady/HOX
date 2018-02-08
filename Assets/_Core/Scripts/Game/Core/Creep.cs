using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] class CreepVisual : TypedMap<GameData.CreepType, GameObject> {}

public class Creep : Character {

	[SerializeField]
	CreepVisual m_creepVisual = null;

	[SerializeField]
	MapCreepData m_creepData;

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
		updateVisual();
	}

	public void updateVisual()
	{
		if (m_activeVisual != null)
			Destroy(m_activeVisual.gameObject);

		m_activeVisual = GameObject.Instantiate(m_creepVisual[m_creepData.type], transform, false);
	}
}
