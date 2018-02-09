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

	Hero m_targetHero = null;

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
		updateVisual();
	}

	public void updateVisual()
	{
		if (m_activeVisual != null)
			DestroyImmediate(m_activeVisual.gameObject);

		m_activeVisual = GameObject.Instantiate(m_creepVisual[m_creepData.type], transform, false);
	}

	void Update()
	{
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == k.Tags.PLAYER) {
			m_targetHero = other.GetComponent<Hero>();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == m_targetHero.gameObject) {
			m_targetHero = null;
		}
			
	}

	void runAnimation()
	{
	}
}
