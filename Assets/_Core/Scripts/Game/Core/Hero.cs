using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable] class HeroVisual : TypedMap<GameData.HeroType, GameObject> { }
[System.Serializable] class HeroPhysics : TypedMap<GameData.HeroType, BasicPhysicalModel> { }

public class Hero : Character, IPunObservable
{
    public System.Action OnFightStarted = null;
    public System.Action OnFightFinished = null;

    [SerializeField]
    Player m_playerPrefab = null;

    [SerializeField]
    tk2dSprite m_domaineSprite = null;

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

    bool m_isInit = false;

    public override GameData.CharacterType getType()
    {
        return GameData.CharacterType.HERO;
    }

    public override Hero getHero()
    {
        return this;
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

        updateVisual();

        if (!Application.isPlaying)
            return;

        initialize(CommonTraits.create(type, 1), new Inventory());

        var gameController = FindObjectOfType<GameController>();
        this.OnDeath += gameController.onPlayerDeath;

        m_domaineSprite.SetSprite(isPlayer ? "color_heros copy" : "color_enemies_heros copy");

        if (isPlayer) {
            // gameObject.AddComponent<Player>();
            m_services.getService<InventoryObserver>().initialize(this);
            m_services.getService<ExperienceObserver>().initialize(this);
        } else {
            m_services.getService<EnemyInventoryObserver>().initialize(this);
        }

        var dataProxy = FindObjectOfType<GameDataProxy>();

        if (dataProxy.team == team)
        {
            var player = PhotonHelper.Instantiate(m_playerPrefab, Vector3.zero, Quaternion.identity, 0);
            player.transform.SetParent(transform, false);
            player.GetComponent<Player>().initialize(this, m_team);
        }
    }

    public void updateVisual()
    {
        if (m_activeVisual != null)
            DestroyImmediate(m_activeVisual.gameObject);

        unsubscribe();

        if (m_activePhysics != null)
            DestroyImmediate(m_activePhysics.gameObject);

        m_activeVisual = GameObject.Instantiate(m_heroVisual[m_type], transform, false);
        m_activePhysics = GameObject.Instantiate(m_heroPhysics[m_type], transform, false);

        specializeDomaine(m_activeVisual, GameData.DomaineType.NONE);

		m_activePhysics.targetObject = gameObject;
        m_activePhysics.OnEnterTrigger += onTriggerEnter;
        m_activePhysics.OnExitTrigger += onTriggerExit;

        if (OnPhysicsInitialized != null)
            OnPhysicsInitialized(m_activePhysics);
    }

    void onTriggerEnter(Collider other, GameObject otherObject)
    {
        var character = otherObject.GetComponent<Character>();
        if (character != null) {
            m_attackTarget = character;
            OnFightStarted?.Invoke();
        }
    }

    void onTriggerExit(Collider other, GameObject otherObject)
    {
        if (m_attackTarget != null && otherObject == m_attackTarget.gameObject) {
            loseAttackTarget();
        }
    }

    public override void onTargetKilled(Character target)
    {
        loseAttackTarget();
        if (!PhotonHelper.isMine(this)) {
            return;
        }
        m_services.getService<PopupController>().openLootPopup(this, target);
        base.onTargetKilled(target);
    }

    protected override void onLevelUp()
    {
        m_data = CommonTraits.create(type, m_data.level + 1);
        base.onLevelUp();
    }

    protected override void onInventoryUpdated(List<Item> items)
    {
        specializeDomaine(m_activeVisual, m_services.getService<LogicController>().getDomaine(this));
        base.onInventoryUpdated(items);
    }

    protected override void onDeathAnimation()
    {
        GameObject.Instantiate(m_deathAnimationPrefab, transform.position, Quaternion.identity);
        base.onDeathAnimation();
    }

    private void loseAttackTarget()
    {
        m_attackTarget = null;
        OnFightFinished?.Invoke();
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
            if (!m_isInit) {
                initialize(Vector3.zero, m_team, type, false);
                m_isInit = true;
            }
        }
    }

    public override void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        base.photonInit();
        // if (photonView.isMine)
        // {
        //     gameObject.AddComponent<Player>();
        // }

    }

    #endregion
}

