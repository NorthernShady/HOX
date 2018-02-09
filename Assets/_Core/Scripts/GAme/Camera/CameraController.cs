using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public enum CameraType
	{
		FOLLOWING,
		FREE
	}

	[SerializeField]
	CameraFollower m_follower = null;

	[SerializeField]
	float m_cameraSpeed = 0.01f;

	float m_cameraShift = 0;
	Vector2 m_drag = Vector2.zero;
	int m_dragIndex = -1;

	CameraType m_cameraType = CameraType.FOLLOWING;

	GameInputController m_gameInputController = null;

	public Vector3 position {
		get {
			return new Vector3(0.0f, transform.position.y, -m_cameraShift);
		}
	}

	void Awake()
	{
		m_cameraShift = transform.position.y / Mathf.Tan(transform.eulerAngles.x * Mathf.Deg2Rad);
		transform.position = new Vector3(0.0f, transform.position.y, -m_cameraShift);
		onUpdatePosition();

		m_gameInputController = FindObjectOfType<GameInputController>();
	}

	void OnEnable()
	{
		if (m_follower != null) {
			m_follower.OnPositionChanged -= onUpdatePosition;
			m_follower.OnPositionChanged += onUpdatePosition;
		}

		m_gameInputController.OnDraggingStarted += onDraggingStarted;
		m_gameInputController.OnDragging += onDragging;
		m_gameInputController.OnDraggingFinished += onDraggingFinished;
	}

	void OnDisable()
	{
		m_gameInputController.OnDraggingStarted -= onDraggingStarted;
		m_gameInputController.OnDragging -= onDragging;
		m_gameInputController.OnDraggingFinished -= onDraggingFinished;

		if (m_follower != null)
			m_follower.OnPositionChanged -= onUpdatePosition;
	}

	public void setFollower(CameraFollower follower) {
		m_follower = follower;
		follower.OnPositionChanged += onUpdatePosition;
		onUpdatePosition();
	}

	public void setCameraType(CameraType cameraType) {
		m_cameraType = cameraType;

		if (m_cameraType == CameraType.FOLLOWING && m_follower != null) {
			onUpdatePosition(m_follower.transform.position);
		}
	}

	void onUpdatePosition()
	{
		if (m_follower != null)
			onUpdatePosition(m_follower.transform.position);
	}

	void onUpdatePosition(Vector3 position)
	{
		if (m_cameraType == CameraType.FOLLOWING)
			transform.position = new Vector3(position.x, transform.position.y, position.z - m_cameraShift);
	}

	void onDraggingStarted(Vector2 position, int index)
	{
		m_cameraType = CameraType.FREE;

		if (m_dragIndex != -1)
			return;

		m_drag = position;
		m_dragIndex = index;
	}

	void onDragging(Vector2 position, int index)
	{
		if (m_dragIndex != index)
			return;

		updatePosition(position);
	}

	void onDraggingFinished(Vector2 position, int index)
	{
		if (m_dragIndex != index)
			return;

		updatePosition(position);
		m_dragIndex = -1;
	}

	void updatePosition(Vector2 position)
	{
		var drag = m_cameraSpeed * (m_drag - position);
		m_drag = position;
		transform.position += new Vector3(drag.x, 0.0f, drag.y);
	}
}
