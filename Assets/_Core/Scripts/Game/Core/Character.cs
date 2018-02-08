using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : Photon.MonoBehaviour {

	[SerializeField]
	float m_speed = 1.0f;

	public virtual void moveTo(Vector3 position)
	{
		transform.DOKill();

		position.y = transform.position.y;
		transform.rotation = Quaternion.LookRotation(position - transform.position, Vector3.up);
		transform.DOMove(position, m_speed).SetSpeedBased();
	}
}
