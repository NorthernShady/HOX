using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

	public System.Action<Vector3> OnPositionChanged;

	Vector3 m_position;

	void Awake() {
		m_position = transform.position;
	}

	void Start() {
	}
	
	// Update is called once per frame
	void Update () {

		if (m_position != transform.position) {
			m_position = transform.position;
			if (OnPositionChanged != null)
				OnPositionChanged(m_position);
		}
	}
}
