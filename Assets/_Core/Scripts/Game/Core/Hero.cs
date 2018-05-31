﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable] class HeroVisual : TypedMap<GameData.HeroType, GameObject> { }
[System.Serializable] class HeroPhysics : TypedMap<GameData.HeroType, BasicPhysicalModel> { }

public class Hero : Character, IPunObservable
{

    [SerializeField]
    HeroPhysics m_heroPhysics = null;

    [SerializeField]
    HeroVisual m_heroVisual = null;

    [SerializeField]
    GameObject m_deathAnimationPrefab;

    [SerializeField]
    GameData.HeroType m_type = GameData.HeroType.WARRIOR;

    [SerializeField]
    int m_team = 0;

    public override GameData.CharacterType getType()
    {
        return GameData.CharacterType.HERO;
    }

    public Vector2 startPosition
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.z);
        }
    }

    public GameData.HeroType type
    {
        get
        {
            return m_type;
        }
        set
        {
            m_type = value;
            updateVisual();
        }
    }

    public int team
    {
        get
        {
            return m_team;
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
        if (m_activePhysics != null)
        {
            m_activePhysics.OnEnterTrigger -= onTriggerEnter;
            m_activePhysics.OnExitTrigger -= onTriggerExit;
        }
    }

    public void initialize(Vector2 position, int team, GameData.HeroType type, bool isPlayer = false)
    {
        m_type = type;
        m_team = team;
        transform.position = new Vector3(position.x, 0.0f, position.y);

        initialize(CommonTraits.create(type, 1), new Inventory());

        var gameController = FindObjectOfType<GameController>();
        this.OnDeath += gameController.onPlayerDeath;

        updateVisual();

        if (isPlayer) {
            gameObject.AddComponent<Player>();
            m_services.getService<InventoryObserver>().initialize(this);
            m_services.getService<ExperienceObserver>().initialize(this);
        }
    }

    public void updateVisual()
    {
        if (m_activeVisual != null)
            DestroyImmediate(m_activeVisual.gameObject);

        unsubscribe();

        m_activeVisual = GameObject.Instantiate(m_heroVisual[m_type], transform, false);
        m_activePhysics = GameObject.Instantiate(m_heroPhysics[m_type], transform, false);

		m_activePhysics.targetObject = gameObject;
        m_activePhysics.OnEnterTrigger += onTriggerEnter;
        m_activePhysics.OnExitTrigger += onTriggerExit;

        if (OnPhysicsInitialized != null)
            OnPhysicsInitialized(m_activePhysics);
    }

    void onTriggerEnter(Collider other, GameObject otherObject)
    {
        var character = otherObject.GetComponent<Character>();
        if (character != null)
            m_attackTarget = character;
    }

    void onTriggerExit(Collider other, GameObject otherObject)
    {
        if (m_attackTarget != null && otherObject == m_attackTarget.gameObject) {
            m_attackTarget = null;
        }
    }

    public override void onTargetKilled(Character target)
    {
        m_services.getService<PopupController>().openLootPopup(this, target);
        base.onTargetKilled(target);
    }

    protected override void onLevelUp()
    {
        m_data = CommonTraits.create(type, m_data.level + 1);
        base.onLevelUp();
    }

    protected override void onDeathAnimation()
    {
        GameObject.Instantiate(m_deathAnimationPrefab, transform.position, Quaternion.identity);
        base.onDeathAnimation();
    }

    #region IPunObservable implementation

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.photonUpdate(stream, info);
        if (stream.isWriting)
        {
            stream.SendNext(team);
            stream.SendNext(type);
        }
        if (stream.isReading)
        {
            m_team = (int)stream.ReceiveNext();
            type = (GameData.HeroType)stream.ReceiveNext();
        }
    }

    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        base.photonInit();
        if (photonView.isMine)
        {
            gameObject.AddComponent<Player>();
        }

    }

    #endregion
}

