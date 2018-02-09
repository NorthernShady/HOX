using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : Photon.PunBehaviour {

	public System.Action<float> OnHealthChanged;
	public System.Action<Character> OnDeath;

	protected CharacterData m_data = null;
	float m_health = 0.0f;
	float m_attackTimer = 0.0f;

	protected bool m_isDead = false;
	protected bool m_shouldAttack = false;
	protected bool m_shouldDestroy = false;

	protected Character m_attackTarget = null;

	bool m_shouldSendAttack = false;

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

		position.y = GetComponent<Rigidbody>().position.y;
		GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(transform.position - position));
		GetComponent<Rigidbody>().DOMove(position, m_data.moveSpeed).SetSpeedBased();
	}

	void Update()
	{
		if (photonView != null && !photonView.isMine) {
			return;
		}
		var canAttack = updateAttackTime();

		if (canAttack && m_attackTarget != null && !m_isDead) {
			attack (m_attackTarget);
			m_shouldSendAttack = true;
		}
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
		onAttackAnimation();

		target.takeDamage(this, m_data.attack);
	}

	void whenHpZero()
	{
		m_isDead = true;
		if (OnDeath != null) {
			OnDeath (this);
		}
		onDeathAnimation ();
	}

	public bool takeDamage(Character target, float amount)
	{
		var damage = Mathf.Max(amount - m_data.defence, 1.0f);
		m_health = Mathf.Max(0.0f, m_health - damage);

		if (OnHealthChanged != null)
			OnHealthChanged(m_health / m_data.maxHealth);

		if (m_health <= 0.01f && photonView.isMine) {
			whenHpZero ();
			m_shouldDestroy = true;
			return true;
		}

		return false;
	}

	protected virtual void onDeathAnimation()
	{
		PhotonNetwork.Destroy(gameObject);
	}

	protected virtual void onAttackAnimation()
	{
		var attackPrefab = Resources.Load<GameObject>(k.Resources.VFXHIT);
		var attack = GameObject.Instantiate(attackPrefab, transform.position, Quaternion.identity);
		Destroy(attack, 0.5f);
	}

	protected void photonUpdate (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (m_shouldSendAttack);
			stream.SendNext (m_shouldDestroy);
			stream.SendNext (m_health);
			m_shouldSendAttack = false;
			m_shouldDestroy = false;
		}
		if (stream.isReading) {
			if ((bool)stream.ReceiveNext () && m_attackTarget != null && !m_isDead) {
				attack (m_attackTarget);
			}
			bool m_shouldDestroy = (bool)stream.ReceiveNext ();
			if (m_shouldDestroy) {
				whenHpZero ();
			}
			m_shouldDestroy = false;
			m_health = (float)stream.ReceiveNext ();
			if (m_data != null) {
				OnHealthChanged (m_health / m_data.maxHealth);
			}
		}
	}
}

