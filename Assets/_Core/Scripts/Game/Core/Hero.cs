using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

[System.Serializable] class HeroVisual : TypedMap<GameData.HeroType, GameObject> { }
[System.Serializable] class HeroPhysics : TypedMap<GameData.HeroType, BasicPhysicalModel> { }
[System.Serializable] class HeroSkills : TypedMap<GameData.HeroType, Skills> { }

public class Hero : Character, IPunObservable
{
    public System.Action OnFightStarted = null;
    public System.Action OnFightFinished = null;

    [SerializeField]
    Player m_playerPrefab = null;

    [SerializeField]
    GameObject m_pointLight = null;

    [SerializeField]
    HeroPhysics m_heroPhysics = null;

    [SerializeField]
    HeroVisual m_heroVisual = null;

    [SerializeField]
    HeroSkills m_heroSkills = null;

    [SerializeField]
    GameObject m_deathAnimationPrefab;

    [SerializeField]
    GameData.HeroType m_type = GameData.HeroType.WARRIOR;

    [SerializeField]
    int m_team = 0;
    GameDataProxy m_dataProxy = null;

    [HideInInspector]
    public Player m_player = null;

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
    List<BasicSkill> m_activeSkills = new List<BasicSkill>();
    Animator m_animator = null;

    public override BasicPhysicalModel getPhysicalModel()
    {
        return m_activePhysics;
    }

    public override List<BasicSkill> getSkills()
    {
        return m_activeSkills;
    }

    void unsubscribe()
    {
        if (m_activePhysics != null)
        {
            m_activePhysics.OnEnterTrigger -= onTriggerEnter;
            m_activePhysics.OnExitTrigger -= onTriggerExit;
        }

        foreach (var skill in m_activeSkills) {
            skill.OnActivated -= onSkillStateChanged;
            skill.OnDeactivated -= onSkillStateChanged;
        }
    }

    public void initialize(Vector2 position, int team, GameData.HeroType type, int characterId, bool isPlayer = false)
    {
        m_dataProxy = FindObjectOfType<GameDataProxy>();
        m_type = type;
        m_team = team;
        transform.position = new Vector3(position.x, 0.0f, position.y);

        updateVisual();

        if (!Application.isPlaying)
            return;

        initialize(CommonTraits.create(type, 1), new Inventory(), characterId);

        var gameController = FindObjectOfType<GameController>();
        this.OnDeath += gameController.onPlayerDeath;

        // m_domaineSprite.SetSprite(isPlayer ? "color_heros copy" : "color_enemies_heros copy");

        if (m_dataProxy.team == team) {
            // gameObject.AddComponent<Player>();
            m_services.getService<InventoryObserver>().initialize(this);
            m_services.getService<ExperienceObserver>().initialize(this);
            m_services.getService<FeaturesObserver>().initialize(this);
        } else {
            m_services.getService<EnemyInventoryObserver>().initialize(this);
        }

        if (m_dataProxy.team == team)
        {
            if (m_dataProxy.lightType == LightType.Point) {
                var light = GameObject.Instantiate(m_pointLight);
                light.transform.SetParent(transform, false);
            }
            var player = PhotonHelper.Instantiate(m_playerPrefab, Vector3.zero, Quaternion.identity, 0);
            player.transform.SetParent(transform, false);
            player.GetComponent<Player>().initialize(this, m_team);
            m_player = player.GetComponent<Player>();
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

        var skills = m_heroSkills[m_type].skills;
        skills.ForEach(x => {
            var activeSkill = GameObject.Instantiate(x, transform, false);
            activeSkill.OnActivated += onSkillStateChanged;
            activeSkill.OnDeactivated += onSkillStateChanged;
            m_activeSkills.Add(activeSkill);
        });

        specializeDomaine(m_activeVisual, GameData.DomaineType.NONE);

		m_activePhysics.targetObject = gameObject;
        m_activePhysics.OnEnterTrigger += onTriggerEnter;
        m_activePhysics.OnExitTrigger += onTriggerExit;
        m_animator = m_activeVisual.GetComponent<Animator>();

        if (OnPhysicsInitialized != null)
            OnPhysicsInitialized(m_activePhysics);
    }

    void onTriggerEnter(Collider other, GameObject otherObject)
    {
        var character = otherObject.GetComponent<Character>();
        if (character != null) {
            m_attackTarget = character;
            m_animator.SetTrigger("Attack");
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
        if (!PhotonHelper.isMaster()) {
            return;
        }
        sendCommandOpenLootPopup(target);
        base.onTargetKilled(target);
    }

    void sendCommandOpenLootPopup(Character target)
    {
        if (isMineHero()) {
            openLootPopup(target.characterId);
        } else {
            m_player.addCommand(Player.CMD_OPEN_LOOT_POPUP, new List<object>() { target.characterId });
        }
    }

    public void openLootPopup(int charId)
    {
        var list = FindObjectsOfType<Character>().ToList();
        var target = list.Find(x => x.characterId == charId);
        if (target != null) {
            m_services.getService<PopupController>().openLootPopup(this, target, m_player);
        }
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
        var deathAnimation = GameObject.Instantiate(m_deathAnimationPrefab, transform.position, Quaternion.identity);
        Destroy(deathAnimation, 2.0f);
        base.onDeathAnimation();
    }

    private void loseAttackTarget()
    {
        m_attackTarget = null;
        m_animator.SetTrigger("Attack");
        OnFightFinished?.Invoke();
    }

    public bool isMineHero()
    {
        if (m_dataProxy == null) {
            m_dataProxy = FindObjectOfType<GameDataProxy>();
        }
        return m_dataProxy.team == m_team;
    }

    #region IPunObservable implementation

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        base.photonUpdate(stream, info);
        if (stream.isWriting)
        {
            stream.SendNext(team);
            stream.SendNext(type);
            stream.SendNext(characterId);
        }
        if (stream.isReading)
        {
            m_team = (int)stream.ReceiveNext();
            type = (GameData.HeroType)stream.ReceiveNext();
            characterId = (int)stream.ReceiveNext();
          
            if (!m_isInit) {
                initialize(Vector3.zero, m_team, type, characterId, false);
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

