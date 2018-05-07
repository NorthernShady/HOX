using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObserver : MonoBehaviour
{
	[SerializeField]
	LayerMask m_layerMask;

	GameInputController m_gameInputController = null;

	Vector2 m_drag = Vector2.zero;
	int m_dragIndex = -1;

	void Awake()
	{
		m_gameInputController = FindObjectOfType<GameInputController>();
	}

	void OnEnable()
	{
		m_gameInputController.OnDraggingStarted += onDraggingStarted;
		m_gameInputController.OnDragging += onDragging;
		m_gameInputController.OnDraggingFinished += onDraggingFinished;
	}

	void OnDisable()
	{
		m_gameInputController.OnDraggingStarted -= onDraggingStarted;
		m_gameInputController.OnDragging -= onDragging;
		m_gameInputController.OnDraggingFinished -= onDraggingFinished;
	}

	void onDraggingStarted(Vector2 position, int index, GameObject targetObject)
	{
		if (m_dragIndex != -1 || targetObject.layer != m_layerMask)
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
		var drag = m_drag - position;
		m_drag = position;
	}
}
