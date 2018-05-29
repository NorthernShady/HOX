using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : Photon.PunBehaviour
{

    public System.Action<float> OnHealthChanged;
    public System.Action<BasicPhysicalModel> OnPhysicsInitialized;
    public System.Action<Character> OnDeath;

    public virtual GameData.CharacterType getType()
    {
        return GameData.CharacterType.NONE;
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

    float m_health = 0.0f;
    float m_attackTimer = 0.0f;

    protected bool m_isDead = false;
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
        m_totalData = m_data.resolve();
        m_health = m_data.maxHealth;
        m_inventory = inventory;

        m_inventory.OnItemsChanged += onInventoryUpdated;
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

        if (canAttack && m_attackTarget != null && !m_isDead)
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
        onAttackAnimation();

        var attack = m_services.getService<LogicController>().countDamage(this, target);
        target.takeDamage(this, attack.Item2, attack.Item1);
    }

    void whenHpZero()
    {
        if (m_isDead)
            return;

        m_isDead = true;
        if (OnDeath != null)
        {
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

        if (m_health <= 0.01f && PhotonHelper.isMine(this))
        {
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
        PhotonHelper.Destroy(gameObject);
        // StartCoroutine(destroyIn(0.5f));
    }

    protected virtual void onInventoryUpdated(List<Item> items)
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
    }

    protected virtual void onAttackAnimation()
    {
        var attackPrefab = Resources.Load<GameObject>(k.Resources.VFXHIT);
        var attack = GameObject.Instantiate(attackPrefab, m_attackTarget.rigidbody.position, m_attackTarget.rigidbody.rotation);
        Destroy(attack, 0.5f);
    }

    protected void photonUpdate(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(m_shouldSendAttack);
            stream.SendNext(m_shouldDestroy);
            stream.SendNext(m_health);
            m_shouldSendAttack = false;
            m_shouldDestroy = false;
        }
        if (stream.isReading)
        {
            if ((bool)stream.ReceiveNext() && m_attackTarget != null && !m_isDead)
            {
                attack(m_attackTarget);
            }
            bool m_shouldDestroy = (bool)stream.ReceiveNext();
            if (m_shouldDestroy)
            {
                whenHpZero();
            }
            m_shouldDestroy = false;
            m_health = (float)stream.ReceiveNext();
            if (m_data != null)
            {
                OnHealthChanged(m_health / m_data.maxHealth);
            }
        }
    }
}

