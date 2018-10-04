using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraFollower : MonoBehaviour {

	public System.Action<Vector3> OnPositionChanged;

	private Vector3 m_position;
	private List<CameraController> m_viewCameras = new List<CameraController>();

	void Awake() {
		m_position = transform.position;
		var cameras = FindObjectsOfType<CameraController>().ToList();
		cameras.ForEach(x => x.setFollower(this));
		m_viewCameras = cameras.FindAll(x => x.isViewCamera);
	}
	
	// Update is called once per frame
	void Update () {

		if (m_position != transform.position) {
			m_position = transform.position;
			if (OnPositionChanged != null)
				OnPositionChanged(m_position);
		}
	}

	public void runCloseAnimation()
	{
		m_viewCameras.ForEach(x => x.runCloseAnimation());
	}

	public void runDistanceAnimation()
	{
		m_viewCameras.ForEach(x => x.runDistanceAnimation());
	}
}
