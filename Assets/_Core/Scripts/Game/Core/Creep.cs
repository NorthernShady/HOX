using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable] class CreepVisual : TypedMap<GameData.CreepType, GameObject> {}
[System.Serializable] class CreepPhysics : TypedMap<GameData.CreepType, BasicPhysicalModel> {}

public class Creep : Character, IPunObservable {

	[SerializeField]
	CreepPhysics m_creepPhysics = null;

	[SerializeField]
	CreepVisual m_creepVisual = null;

	[SerializeField]
	MapCreepData m_creepData;
	
	Transform m_hero = null;

	bool isInit = false;

	public override GameData.CharacterType getType()
    {
        return GameData.CharacterType.CREEP;
    }

	protected override void Awake()
	{
		base.Awake();
		m_hero = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public MapCreepData creepData {
		get {
			m_creepData.position = new Vector2(transform.position.x, transform.position.z);
			return m_creepData;
		}
	}

	GameObject m_activeVisual = null;
	BasicPhysicalModel m_activePhysics = null;

	public override BasicPhysicalModel getPhysicalModel()
    {
        return m_activePhysics;
    }

	void unsubscribe()
	{
		if (m_activePhysics != null) {
			m_activePhysics.OnEnterTrigger -= onTriggerEnter;
			m_activePhysics.OnExitTrigger -= onTriggerExit;
		}
	}

	public void initialize(MapCreepData creepData)
	{
		m_creepData = creepData;
		transform.position = new Vector3(m_creepData.position.x, 0.0f, m_creepData.position.y);
		initialize(CommonTraits.create(m_creepData.type, m_creepData.level), createInventory());

		updateVisual();
		isInit = true;
	}

	public void updateVisual()
	{
		if (m_activeVisual != null)
			DestroyImmediate(m_activeVisual.gameObject);

		unsubscribe();

		m_activeVisual = GameObject.Instantiate(m_creepVisual[m_creepData.type], transform, false);
		m_activePhysics = GameObject.Instantiate(m_creepPhysics[m_creepData.type], transform, false);

		// m_activeVisual.transform.SetParent(transform, false);
		// m_activePhysics.transform.SetParent(transform, false);

		m_activePhysics.targetObject = gameObject;
		m_activePhysics.OnEnterTrigger += onTriggerEnter;
		m_activePhysics.OnExitTrigger += onTriggerExit;

		if (OnPhysicsInitialized != null)
            OnPhysicsInitialized(m_activePhysics);
	}

	void onTriggerEnter(Collider other, GameObject otherObject)
	{
		if (otherObject.tag == k.Tags.PLAYER) {
			m_attackTarget = otherObject.GetComponent<Character>();
		}
	}

	void onTriggerExit(Collider other, GameObject otherObject)
	{
		if (m_attackTarget != null && otherObject == m_attackTarget.gameObject) {
			m_attackTarget = null;
		}
	}

	protected override void onDeathAction()
    {
        base.onDeathAction();
		m_attackTarget.onTargetKilled(this);
    }

	void runAnimation()
	{
	}

	Inventory createInventory()
	{
		var items = new List<Item>();

		var itemType = EnumHelper.Random<GameData.ItemType>();
		//(GameData.ItemType)Random.Range(1, 11);

		items.Add(new Item(itemType, m_creepData.domaine));
		return new Inventory(items);
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

