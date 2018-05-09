using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DragObserverDelegate
{
	void draggingStarted(GameObject targetObject, Vector2 position);
	void dragging(GameObject targetObject, Vector2 position);
	void draggingFinished(GameObject targetObject, Vector2 position);
	// public abstract GameObject getDragObjectForTarget(GameObject obj);
	// public virtual void updateDragPosition(Vector2 dragPosition, Vector2 offset){}
	// public abstract void endDraging(GameObject obj, Vector2 dragPosition);
	// public virtual void endDragingPos(Vector2 dragPosition){}
}

public class Drag
{
	public int index;
	public Vector2 position;
	public GameObject targetObject;

	public Drag(int index, Vector2 position, GameObject targetObject)
	{
		this.index = index;
		this.position = position;
		this.targetObject = targetObject;
	}
}

public class DragObserver : MonoBehaviour
{
	[SerializeField]
	LayerMask m_layerMask;

	DragObserverDelegate m_dragDelegate = null;
	public DragObserverDelegate dragDelegate {
		set {
			m_dragDelegate = value;
		}
	}

	GameInputController m_gameInputController = null;

	List<Drag> m_drags = new List<Drag>();

	void Awake()
	{
		m_gameInputController = FindObjectOfType<GameInputController>();
	}

	void OnEnable()
	{
		m_gameInputController.AddLayerMask(m_layerMask);
		m_gameInputController.OnDraggingStarted += onDraggingStarted;
		m_gameInputController.OnDragging += onDragging;
		m_gameInputController.OnDraggingFinished += onDraggingFinished;
	}

	void OnDisable()
	{
		m_gameInputController.OnDraggingStarted -= onDraggingStarted;
		m_gameInputController.OnDragging -= onDragging;
		m_gameInputController.OnDraggingFinished -= onDraggingFinished;
		m_gameInputController.RemoveLayerMask(m_layerMask);
	}

	void onDraggingStarted(Vector2 position, int index, GameObject targetObject)
	{
		if (targetObject.layer != m_layerMask)
			return;

		m_drags.Add(new Drag(index, position, targetObject));
		m_dragDelegate.draggingStarted(targetObject, position);
	}

	void onDragging(Vector2 position, int index)
	{
		var drag = m_drags.Find(x => x.index == index);

		if (drag == null)
			return;

		updatePosition(drag, position);
		m_dragDelegate.dragging(drag.targetObject, drag.position);
	}

	void onDraggingFinished(Vector2 position, int index)
	{
		var drag = m_drags.Find(x => x.index == index);

		if (drag == null)
			return;

		updatePosition(drag, position);
		m_drags.Remove(drag);
		m_dragDelegate.draggingFinished(drag.targetObject, drag.position);
	}

	void updatePosition(Drag drag, Vector2 position)
	{
		drag.position = drag.position - position;
	}
}
