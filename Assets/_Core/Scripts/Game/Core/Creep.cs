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

		updateVisual();

		if (!Application.isPlaying)
			return;

		initialize(CommonTraits.create(m_creepData.type, m_creepData.level), createInventory());
		isInit = true;
	}

	public void updateVisual()
	{
		if (m_activeVisual != null)
			DestroyImmediate(m_activeVisual.gameObject);

		unsubscribe();

		m_activeVisual = GameObject.Instantiate(m_creepVisual[m_creepData.type], transform, false);
		m_activePhysics = GameObject.Instantiate(m_creepPhysics[m_creepData.type], transform, false);

		specializeDomaine(m_activeVisual, m_creepData.domaine);

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

		var cellDrop = CharacterConfigDBHelper.getDomaineConfig("CellDrop");
		var colorDrop = CharacterConfigDBHelper.getDomaineConfig("ColorDrop");
		var levelDrop = CharacterConfigDBHelper.getDomaineConfig("LevelDrop");

		if (Random.Range(0.0f, 1.0f) <= cellDrop.Disadvantage) {
			var domaine = getItemDomaineDrop(colorDrop);
			var level = Mathf.Max(0, m_creepData.level + getItemLevelDrop(levelDrop));
			var possibleItems = CharacterConfigDBHelper.getNonConsumableItemConfigs(level);

			if (possibleItems.Count != 0) {
				var item = possibleItems[Random.Range(0, possibleItems.Count)];
				items.Add(new Item(item.Name.ToEnum(GameData.ItemType.NONE), domaine, new CommonTraits(item)));
			}
		}

		if (Random.Range(0.0f, 1.0f) <= cellDrop.Equal) {
			items.Add(new Item(GameData.ItemType.POTION_HEAL, GameData.DomaineType.INDIFFERENT));
		}

		if (Random.Range(0.0f, 1.0f) <= cellDrop.Advantage) {
			var domaine = getItemDomaineDrop(colorDrop);
			var level = Mathf.Max(1, m_creepData.level + getItemLevelDrop(levelDrop));
			var possibleItems = CharacterConfigDBHelper.getNonConsumableItemConfigs(level);

			if (possibleItems.Count != 0) {
				var item = possibleItems[Random.Range(0, possibleItems.Count)];
				items.Add(new Item(item.Name.ToEnum(GameData.ItemType.NONE), domaine, new CommonTraits(item)));
			}
		}

		return new Inventory(items);
	}

	int getItemLevelDrop(DomaineConfig levelDrop)
	{
		var random = Random.Range(0.0f, levelDrop.Disadvantage + levelDrop.Equal + levelDrop.Advantage);
		
		if (random < levelDrop.Disadvantage)
			return -1;

		if (random < levelDrop.Disadvantage + levelDrop.Equal)
			return 0;

		return 1;
	}

	GameData.DomaineType getItemDomaineDrop(DomaineConfig colorDrop)
	{
		var random = Random.Range(0.0f, colorDrop.Disadvantage + colorDrop.Equal + colorDrop.Advantage);

		if (random < colorDrop.Disadvantage)
			return m_creepData.domaine.DisadvantageDomaine();

		if (random < colorDrop.Disadvantage + colorDrop.Equal)
			return m_creepData.domaine;

		return m_creepData.domaine.AdvantageDomaine();
	}

	// var total = dataDictionary.Values.Sum(item => item.dropChance);
    //     var random = Random.Range(0, total);
    //     float value = 0;

    //     foreach (var key in dataDictionary.Keys)
    //     {
    //         if (random < (dataDictionary[key].dropChance + value))
    //         {
    //             return key;
    //         }
    //         else
    //         {
    //             value = value + dataDictionary[key].dropChance;
    //         }
    //     }

    //     return GameData.GameElementType.TYPE_1;

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
            if (m_creepData == null) {
                m_creepData = new MapCreepData();
            }
			m_creepData.domaine = (GameData.DomaineType)stream.ReceiveNext ();
			m_creepData.level = (int)stream.ReceiveNext ();
			m_creepData.position = (Vector2)stream.ReceiveNext ();
			m_creepData.type = (GameData.CreepType)stream.ReceiveNext ();
			if (!isInit) {
				initialize (m_creepData);
			}
		}
	}

    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        base.photonInit();
    }

	#endregion
}

