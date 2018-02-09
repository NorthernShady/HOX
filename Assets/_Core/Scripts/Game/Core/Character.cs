﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : Photon.MonoBehaviour {

	[SerializeField]
	float m_speed = 1.0f;

	HealthBar m_healthBar = null;

	void Start()
	{
		createHealthBar();
	}

	void OnDelete()
	{
		if (m_healthBar != null)
			Destroy(m_healthBar);
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
		GetComponent<Rigidbody>().DOMove(position, m_speed).SetSpeedBased();
	}

	void OnCollisionEnter(Collision collision)
	{
	}

	protected void doCycleAnimation(Vector3 endPosition)
	{
	}

	void createHealthBar()
	{
		var m_healthBarPrefab = Resources.Load<HealthBar>(k.Resources.HEALTH_BAR);
		m_healthBar = GameObject.Instantiate(m_healthBarPrefab, transform, false);
		//m_healthBar.transform.position += new Vector3(0.0f, 10.0f, 10.0f);
		m_healthBar.initialize();
	}
}
