using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasDragObserver : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	Vector3 m_startPosition = Vector3.zero;
	Transform m_startParent = null;

	public void OnBeginDrag(PointerEventData eventData)
	{
		m_startPosition = transform.position;
		m_startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (transform.parent == m_startParent)
			transform.position = m_startPosition;
	}
}
