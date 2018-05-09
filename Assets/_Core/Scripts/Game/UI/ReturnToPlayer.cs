using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPlayer : MonoBehaviour {

	CameraController m_cameraController = null;

	void Awake()
	{
		m_cameraController = FindObjectOfType<CameraController>();
	}

	public void onClick()
	{
		m_cameraController.setCameraType(CameraController.CameraType.FOLLOWING);
	}
}
