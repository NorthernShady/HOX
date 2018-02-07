using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	[SerializeField]
	CameraFollower m_follower = null;

	public float m_cameraShift = 0;

	void Awake()
	{
		m_cameraShift = transform.position.y / Mathf.Tan(transform.eulerAngles.x * Mathf.Deg2Rad);
		transform.position = new Vector3(0.0f, transform.position.y, -m_cameraShift);
	}

	void OnEnable()
	{
		if (m_follower != null)
			m_follower.OnPositionChanged += onUpdatePosition;
	}

	void OnDisable()
	{
		if (m_follower != null)
			m_follower.OnPositionChanged -= onUpdatePosition;
	}

	public void setFollower(CameraFollower follower) {
		m_follower = follower;
		follower.OnPositionChanged += onUpdatePosition;
	}

	void onUpdatePosition(Vector3 position)
	{
		transform.position = new Vector3(position.x, transform.position.y, position.z - m_cameraShift);
	}
}
