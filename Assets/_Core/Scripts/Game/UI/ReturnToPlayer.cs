using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReturnToPlayer : MonoBehaviour {

	List<CameraController> m_cameraControllers = null;

	void Awake()
	{
		m_cameraControllers = FindObjectsOfType<CameraController>().ToList();
	}

	public void onClick()
	{
		m_cameraControllers.ForEach(x => x.setCameraType(CameraController.CameraType.FOLLOWING));
	}
}
