using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineController : MonoBehaviour {

	[SerializeField]
	CameraController m_cameraController;

	void Awake()
	{
		m_cameraController = Camera.main.GetComponent<CameraController>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var position = m_cameraController.mapPosition;
		transform.position = new Vector3(position.x, transform.position.y, position.z);
	}
}
