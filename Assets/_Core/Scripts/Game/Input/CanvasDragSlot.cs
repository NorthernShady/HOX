using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasDragSlot : MonoBehaviour, IDropHandler {

	public GameObject item {
		get {
			return transform.childCount > 0 ? transform.GetChild(0).gameObject : null;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		var newItem = eventData.pointerDrag.transform;
		var otherParent = newItem.parent;

		item.transform.SetParent(otherParent, false);
		newItem.transform.SetParent(transform);
		newItem.transform.localPosition = Vector3.zero;
	}
}
