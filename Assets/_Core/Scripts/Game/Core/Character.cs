using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : Photon.MonoBehaviour {

	public System.Action<float> OnHealthChanged;
	public System.Action<Character> OnDeath;

	HealthBar m_healthBar = null;

	protected CharacterData m_data = null;
	float m_health = 0.0f;
	float m_attackTimer = 0.0f;

	protected bool m_isDead = false;
	protected bool m_shouldAttack = false;

	protected Character m_attackTarget = null;

	void Start()
	{
//		createHealthBar();
	}

//	void OnDelete()
//	{
//		if (m_healthBar != null)
//			Destroy(m_healthBar);
//	}

	protected void initialize(CharacterData characterData)
	{
		m_data = characterData;
		m_health = m_data.maxHealth;
	}

	public virtual void moveTo(Vector3 position)
	{
		if (!photonView.isMine && PhotonNetwork.connected) {
			return;
		}
		transform.DOKill();

		position.y = transform.position.y;
		//transform.rotation = Quaternion.LookRotation(transform.position - position, Vector3.up);
		//transform.DOMove(position, m_speed).SetSpeedBased();

		position.y = GetComponent<Rigidbody>().position.y;
		GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(transform.position - position));
		GetComponent<Rigidbody>().DOMove(position, m_data.moveSpeed).SetSpeedBased();
	}

	void Update()
	{
		var canAttack = updateAttackTime();

		if (canAttack && m_attackTarget != null && !m_isDead)
			attack(m_attackTarget);
	}

	bool updateAttackTime()
	{
		if (m_attackTimer > m_data.attackSpeed)
			return true;

		m_attackTimer += Time.deltaTime;
		return false;
	}

	public void attack(Character target)
	{
		m_attackTimer = 0.0f;
		target.takeDamage(this, m_data.attack);
	}

	public bool takeDamage(Character target, float amount)
	{
		var damage = Mathf.Max(amount - m_data.defence, 1.0f);
		m_health = Mathf.Max(0.0f, m_health - damage);

		if (OnHealthChanged != null)
			OnHealthChanged(m_health / m_data.maxHealth);

		if (m_health <= 0.01f) {
			m_isDead = true;
			if (OnDeath != null)
				OnDeath(this);

			onDeathAnimation();

			return true;
		}

		return false;
	}

	protected virtual void onDeathAnimation()
	{
	}
}
