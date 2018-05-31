using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBillboard : MonoBehaviour {

	Vector3 m_cameraVector = Vector3.up;

	void Start()
	{
		m_cameraVector = FindObjectOfType<CameraController>().position;
	}

	void Update () {
		transform.rotation = Quaternion.LookRotation(-m_cameraVector);
	}
}
