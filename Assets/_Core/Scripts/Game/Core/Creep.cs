using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable] class CreepVisual : TypedMap<GameData.CreepType, GameObject> {}

public class Creep : Character, IPunObservable {

	[SerializeField]
	CreepVisual m_creepVisual = null;

	[SerializeField]
	MapCreepData m_creepData;
	
	Transform m_hero = null;

	bool isInit = false;

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
		isInit = true;
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
		if (m_attackTarget != null && other.gameObject == m_attackTarget.gameObject) {
			m_attackTarget = null;
		}
	}

	void runAnimation()
	{
	}

	#region IPunObservable implementation

	void IPunObservable.OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		base.photonUpdate (stream, info);
		if (stream.isWriting) {
			stream.SendNext (m_creepData.domaine);
			stream.SendNext (m_creepData.level);
			stream.SendNext (m_creepData.position);
			stream.SendNext (m_creepData.type);
		}
		if (stream.isReading) {
			m_creepData = new MapCreepData ();
			m_creepData.domaine = (GameData.DomaineType)stream.ReceiveNext ();
			m_creepData.level = (int)stream.ReceiveNext ();
			m_creepData.position = (Vector2)stream.ReceiveNext ();
			m_creepData.type = (GameData.CreepType)stream.ReceiveNext ();
			if (!isInit) {
				initialize (m_creepData);
			}
		}
	}

	#endregion
}

