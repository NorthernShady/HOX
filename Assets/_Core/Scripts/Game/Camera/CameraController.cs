using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

	[SerializeField]
	bool m_isViewCamera = true;

	[SerializeField]
	float m_nearCameraSize = 1.0f;

	[SerializeField]
	float m_viewChangeTime = 1.0f;

	private Camera m_camera = null;
	private float m_farCameraSize = 1.0f;
	float m_cameraShift = 0;
	Vector2 m_drag = Vector2.zero;
	int m_dragIndex = -1;
	float m_cameraSize = 0.0f;

	CameraType m_cameraType = CameraType.FOLLOWING;

	GameInputController m_gameInputController = null;

	public bool isViewCamera {
		get {
			return m_isViewCamera;
		}
	}

	public Vector3 position {
		get {
			return new Vector3(0.0f, transform.position.y, -m_cameraShift);
		}
	}

	public Vector3 mapPosition {
		get {
			return new Vector3(transform.position.x, 0.0f, transform.position.z + m_cameraShift);
		}
	}

	void Awake()
	{
		m_camera = GetComponent<Camera>();
		m_cameraSize = m_farCameraSize = m_camera.orthographicSize;
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

	void onDraggingStarted(Vector2 position, int index, GameObject targetObject)
	{
		if (m_dragIndex != -1 || targetObject.layer != k.Layers.MAP)
			return;

		m_cameraType = CameraType.FREE;

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

	public void runCloseAnimation()
	{
		transform.DOKill();
		DOVirtual.Float(m_cameraSize, m_nearCameraSize, m_viewChangeTime, x => distanceCamera(x));
	}

	public void runDistanceAnimation()
	{
		transform.DOKill();
		DOVirtual.Float(m_cameraSize, m_farCameraSize, m_viewChangeTime, x => distanceCamera(x));
	}

	private void distanceCamera(float value)
	{
		m_cameraSize = value;
		m_camera.orthographicSize = m_cameraSize;
	}
}
