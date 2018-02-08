using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : Photon.MonoBehaviour {

	[SerializeField]
	float m_speed = 1.0f;

	[SerializeField]
	HealthBar m_healthBar = null;

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
	}
}
