using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CameraFollower))]
public class Player : MonoBehaviour {

	GameInputController m_gameInputController = null;

	void Awake()
	{
		m_gameInputController = FindObjectOfType<GameInputController>();
	}

	void OnEnable()
	{
		m_gameInputController.OnTap -= onTap;
		m_gameInputController.OnTap += onTap;
	}

	void OnDisable()
	{
		m_gameInputController.OnTap -= onTap;
	}

	// Use this for initialization
	void Start () {
	}
	
	void onTap(Vector3 position)
	{
		GetComponent<Hero>().moveTo(position);
	}
}
