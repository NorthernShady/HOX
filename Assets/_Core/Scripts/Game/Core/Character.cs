using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : Photon.PunBehaviour
{

    [SerializeField]
    GameObject m_mainVisual;

    public System.Action<int, float> OnExpChanged;
    public System.Action<float> OnHealthChanged;
    public System.Action<BasicPhysicalModel> OnPhysicsInitialized;
    public System.Action<Character> OnDeath;

    public virtual GameData.CharacterType getType()
    {
        return GameData.CharacterType.NONE;
    }

    public virtual Hero getHero()
    {
        return null;
    }

    public virtual BasicPhysicalModel getPhysicalModel()
    {
        return null;
    }

    protected Services m_services = null;

    protected Rigidbody m_rigidbody = null;
    public Rigidbody rigidbody {
        get {
            return m_rigidbody;
        }
    }

    protected Inventory m_inventory = null;
    public Inventory inventory {
        get {
            return m_inventory;
        }
    }

    protected CommonTraits m_data = null;
    public CommonTraits data {
        get {
            return m_data;
        }
    }

    protected CommonTraits m_totalData = null;
    public CommonTraits totalData {
        get {
            return m_totalData;
        }
    }

    float m_exp = 0.0f;
    public float expPercent {
        get {
            return m_totalData.exp < 0 ? 1.0f : m_exp / m_totalData.exp;
        }
    }

    float m_health = 0.0f;
    float m_attackTimer = 0.0f;

    protected bool m_isDead = false;
    protected bool m_isAttackAnimated = false;
    protected bool m_shouldAttack = false;
    protected bool m_shouldDestroy = false;

    protected Character m_attackTarget = null;

    bool m_shouldSendAttack = false;

    protected virtual void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_services = FindObjectOfType<Services>();
    }

    void OnDisable()
    {
        if (m_inventory != null)
            m_inventory.OnItemsChanged -= onInventoryUpdated;
    }

    protected void initialize(CommonTraits characterData, Inventory inventory)
    {
        m_data = characterData;
        m_inventory = inventory;
        m_inventory.OnItemsChanged += onInventoryUpdated;

        updateParameters();
        m_health = m_totalData.maxHealth;
    }

    public virtual void moveTo(Vector3 position)
    {
        if (!PhotonHelper.isMine(this))
            return;

		m_rigidbody.DOKill();
        position.y = m_rigidbody.position.y;
        turnTo(position);

		var direction = position - m_rigidbody.position;
		var distance = direction.magnitude;
		RaycastHit raycastHit;
		if (Physics.Raycast(MathHelper.yShift(m_rigidbody.position, 1.2f), MathHelper.yShift(position - m_rigidbody.position, 1.2f), out raycastHit, (position - m_rigidbody.position).magnitude)) {
			position = (raycastHit.distance > 0.5f) ? (raycastHit.distance - 0.5f) * (direction / distance) + m_rigidbody.position :
				m_rigidbody.position;
		}

		GetComponent<Rigidbody>().DOMove(position, m_totalData.moveSpeed).SetSpeedBased();
    }

    void Update()
    {
        if (!PhotonHelper.isMine(this))
            return;
        //		if (photonView != null && !photonView.isMine)

        var canAttack = updateAttackTime();
        var shouldAttack = m_attackTarget != null && !m_isDead;

        if (shouldAttack && !m_isAttackAnimated) {
            StartCoroutine(startAttackAnimation());
        }

        if (canAttack && shouldAttack)
        {
            turnTo(m_attackTarget.rigidbody.position);
            attack(m_attackTarget);
            m_shouldSendAttack = true;
        }
    }

    bool updateAttackTime()
    {
        if (m_attackTimer > 1.0f / m_totalData.attackSpeed)
            return true;

        m_attackTimer += Time.deltaTime;
        return false;
    }

    public void attack(Character target)
    {
        m_attackTimer = 0.0f;
        // onAttackAnimation();

        var attack = m_services.getService<LogicController>().countDamage(this, target);
        target.takeDamage(this, attack.Item2, attack.Item1);

        if (attack.Item1)
            onCriticalAttackAnimation();
    }

    public void useItem(Item item)
    {
        if (item.type == GameData.ItemType.POTION_HEAL)
            heal(item.data.maxHealth);

        m_inventory.consumeItem(item);
    }

    private void heal(float health)
    {
        m_health = Mathf.Min(m_health + health, m_totalData.maxHealth);

        if (OnHealthChanged != null)
            OnHealthChanged(m_health / m_totalData.maxHealth);

        onHealAnimation();
    }

    public void exping(Character target)
    {
        if (m_totalData.exp < 0)
            return;

        m_exp += target.totalData.fightExp;

        Debug.Log("Exp: " + m_exp);

        if (m_exp >= m_totalData.exp) {
            m_exp -= m_totalData.exp;
            onLevelUp();
            onLevelUpAnimation();
        }

        if (OnExpChanged != null)
            OnExpChanged(m_data.level, m_totalData.exp < 0 ? 1.0f : m_exp / m_totalData.exp);
    }

    public void specializeDomaine(GameObject visual, GameData.DomaineType domaine)
    {
        if (domaine == GameData.DomaineType.NONE)
            return;

        var domainePart = visual.transform.Find("DomainePart");

        if (domainePart == null)
            return;
            
        var meshRenderer = domainePart.GetComponent<MeshRenderer>();
        meshRenderer.material = Resources.Load<ItemData>(k.Resources.ITEM_DATA).domaineMaterial[domaine];
    }

    void whenHpZero()
    {
        if (m_isDead)
            return;

        m_isDead = true;
        if (OnDeath != null) {
            OnDeath(this);
        }
        onDeathAction();
        onDeathAnimation();
    }

    protected void turnTo(Vector3 position)
    {
        m_rigidbody.MoveRotation(Quaternion.LookRotation(m_rigidbody.position - position));
    }

    public bool takeDamage(Character target, float amount, bool isCritical)
    {
        Debug.Log(string.Format("{0} took {1} damage, is critical = {2}", getType(), amount, isCritical));
        m_health = Mathf.Max(0.0f, m_health - amount);

        if (OnHealthChanged != null)
            OnHealthChanged(m_health / m_totalData.maxHealth);

        if (m_health <= 0.01f && PhotonHelper.isMine(this)) {
            whenHpZero();
            m_shouldDestroy = true;
            return true;
        }

        return false;
    }

    protected virtual void onDeathAction()
    {
    }

    protected virtual void onDeathAnimation()
    {
        //PhotonHelper.Destroy(gameObject);
        // StartCoroutine(destroyIn(0.5f));
        disableVisualAndLogic();
        if (!PhotonHelper.isConnected()) {
            PhotonHelper.Destroy(gameObject);
        }
    }

    protected virtual void disableVisualAndLogic()
    {
        m_rigidbody.detectCollisions = false;
        m_mainVisual.SetActive(false);
    }

    protected virtual void onInventoryUpdated(List<Item> items)
    {
        updateParameters();
    }

    protected virtual void updateParameters()
    {
        var data = m_data + m_services.getService<LogicController>().countInventory(this);
        m_totalData = data.resolve();
    }

    IEnumerator destroyIn(float time)
    {
        yield return new WaitForSeconds(time);
        PhotonHelper.Destroy(gameObject);
    }

    public virtual void onTargetKilled(Character target)
    {
        exping(target);
    }

    protected virtual void onLevelUp()
    {
        updateParameters();
        heal(m_totalData.maxHealth);
        Debug.Log("Level up");
    }

    #region Action animations

    IEnumerator startAttackAnimation()
    {
        var delay = new WaitForEndOfFrame();
        var prefab = Resources.Load<GameObject>(k.Resources.ATTACK_VFX);
        var attackAnimation = GameObject.Instantiate(prefab, rigidbody.position, Quaternion.identity);
        m_isAttackAnimated = true;

        while (m_attackTarget != null && !m_isDead) {
            yield return delay;
        }

        m_isAttackAnimated = false;
        Destroy(attackAnimation);
    }

    protected virtual void onAttackAnimation()
    {
        var prefab = Resources.Load<GameObject>(k.Resources.ATTACK_VFX);
        var attackAnimation = GameObject.Instantiate(prefab, rigidbody.position, Quaternion.identity);
        Destroy(attackAnimation, 1.0f);

        // var attack = GameObject.Instantiate(attackPrefab, m_attackTarget.rigidbody.position, m_attackTarget.rigidbody.rotation);
    }

    protected virtual void onCriticalAttackAnimation()
    {
        var prefab = Resources.Load<GameObject>(k.Resources.CRITICAL_DAMAGE);
        var criticalAttackAnimation = GameObject.Instantiate(prefab, rigidbody.position, Quaternion.identity);
        Destroy(criticalAttackAnimation, 1.0f);
    }

    protected virtual void onHealAnimation()
    {
        var prefab = Resources.Load<GameObject>(k.Resources.HEALING_VFX);
        var healAnimation = GameObject.Instantiate(prefab, rigidbody.position, Quaternion.identity);
        Destroy(healAnimation, 1.0f);
    }

    protected virtual void onLevelUpAnimation()
    {
        var prefab = Resources.Load<GameObject>(k.Resources.LEVEL_UP_VFX);
        var levelUpAnimation = GameObject.Instantiate(prefab, rigidbody.position, Quaternion.identity);
        Destroy(levelUpAnimation, 1.0f);
    }

    #endregion

    void traitsSerialization(PhotonStream stream)
    {
        var dataString = "";
        if (m_data != null) {
            dataString = JsonUtility.ToJson(m_data);
        }
        stream.SendNext(dataString);
        if (m_inventory != null) {
            var items = m_inventory.items;
            stream.SendNext(items.Count);
            foreach (var item in items) {
                var itemString = JsonUtility.ToJson(m_data);
                stream.SendNext(itemString);
            }
        } else {
            stream.SendNext(0);
        }

    }

    void traitsDeserialization(PhotonStream stream)
    {
        var dataString = (string)stream.ReceiveNext();
        if (dataString != "") {
            var data = JsonUtility.FromJson<CommonTraits>(dataString);
            if (data != null && data.m_traits != null) {
                m_data = data;
            }
        }
        var itemsCount = (int)stream.ReceiveNext();
        var items = new List<Item>();

        for (int i = 0; i < itemsCount; i++) {
            var itemString = (string)stream.ReceiveNext();
            var item = JsonUtility.FromJson<Item>(itemString);
            items.Add(item);
        }

        Debug.Log("items_count: " + itemsCount.ToString());

        if (m_inventory != null) {
            m_inventory.resetItems();
            foreach (var item in items) {
                m_inventory.addItem(item);
            }
        }
    }

    protected void photonUpdate(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting) {
            stream.SendNext(m_shouldSendAttack);
            stream.SendNext(m_shouldDestroy);
            stream.SendNext(m_health);
            traitsSerialization(stream);

            if (m_shouldDestroy) {
                StartCoroutine(timerCallback(1.5f, delegate {
                    PhotonHelper.Destroy(gameObject);
                }));
            }
           

            m_shouldSendAttack = false;
            m_shouldDestroy = false;
        }
        if (stream.isReading) {
            bool shouldAttack = (bool)stream.ReceiveNext();
            bool shouldDestroy = (bool)stream.ReceiveNext();
            m_health = (float)stream.ReceiveNext();
            traitsDeserialization(stream);

            if (shouldAttack && m_attackTarget != null && !m_isDead) {
                attack(m_attackTarget);
            }

            if (shouldDestroy) {
                whenHpZero();
            }
            shouldDestroy = false;

            if (m_data != null) {
                OnHealthChanged?.Invoke(m_health / m_data.maxHealth);
            }
        }
    }

    protected void photonInit()
    {
        var map = FindObjectOfType<BasicGrid>();
        gameObject.transform.SetParent(map.gameObject.transform);
    }

    IEnumerator timerCallback(float time, System.Action callback)
    {
        yield return new WaitForSeconds(time);
        callback();
    }

}

