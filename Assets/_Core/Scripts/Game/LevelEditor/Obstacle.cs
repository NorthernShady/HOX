using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] class ObstacleVisual : TypedMap<GameData.ObstacleType, GameObject> {}
[System.Serializable] class ObstacleSkills : TypedMap<GameData.ObstacleType, Skills> {}

public class Obstacle : MonoBehaviour {

	[SerializeField]
	ObstacleVisual m_obstacleVisual = null;

	[SerializeField]
	ObstacleSkills m_obstacleSkills = null;

	[SerializeField]
	ObstacleData m_obstacleData;

	GameObject m_activeVisual = null;
	List<BasicSkill> m_activeSkills = new List<BasicSkill>();

	public ObstacleData obstacleData {
		get {
			m_obstacleData.position = transform.position;
			m_obstacleData.scale = transform.localScale;
			m_obstacleData.rotation = transform.rotation;
			return m_obstacleData;
		}
	}

	public void initialize(ObstacleData obstacleData)
	{
		m_obstacleData = obstacleData;
		transform.position = m_obstacleData.position;
		transform.localScale = m_obstacleData.scale;
		transform.rotation = m_obstacleData.rotation;

		updateVisual();
	}

	public void updateVisual()
	{
		if (m_activeVisual != null)
			DestroyImmediate(m_activeVisual.gameObject);

		m_activeVisual = GameObject.Instantiate(m_obstacleVisual[m_obstacleData.type], transform, false);

		var skills = m_obstacleSkills[m_obstacleData.type];
		skills.skills.ForEach(x => m_activeSkills.Add(GameObject.Instantiate(x, transform, false)));
	}
}
