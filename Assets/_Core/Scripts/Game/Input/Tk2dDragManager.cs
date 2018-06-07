using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tk2dDragManager : MonoBehaviour {

	[SerializeField]
	Vector2 m_catchBox = Vector2.zero;

	[SerializeField]
	List<GameObject> m_dragSlots = null;

	[SerializeField]
	Tk2dDragObserver m_dragObserverPrefab = null;

	List<Tk2dDragObserver> m_dragObservers = null;

	void Awake()
	{
		m_dragObservers = new List<Tk2dDragObserver>(m_dragSlots.Count);

		var position = new Vector3(0.0f, 0.0f, -0.1f);
		foreach (var dragSlot in m_dragSlots) {
			var dragObserver = GameObject.Instantiate(m_dragObserverPrefab, position, Quaternion.identity);
			dragObserver.transform.SetParent(dragSlot.transform, false);
			m_dragObservers.Add(dragObserver);
		}
	}

	void OnEnable()
	{
		foreach (var observer in m_dragObservers) {
			observer.OnDragFinished += onDragFinished;
		}
	}

	void OnDisable()
	{
		foreach (var observer in m_dragObservers) {
			observer.OnDragFinished -= onDragFinished;
		}
	}

	void onDragFinished(Tk2dDragObserver dragObserver)
	{
		var position = dragObserver.transform.position;

		foreach (var slot in m_dragSlots) {
			if (isCatched(slot, position)) {
				setParent(slot, dragObserver);
				break;
			}
		}
	}

	bool isCatched(GameObject slot, Vector3 observerPosition)
	{
		var slotPosition = slot.transform.position;
		
		if (Mathf.Abs(slotPosition.x - observerPosition.x) > m_catchBox.x * 0.5f)
			return false;

		if (Mathf.Abs(slotPosition.y - observerPosition.y) > m_catchBox.y * 0.5f)
			return false;

		return true;
	}

	void setParent(GameObject slot, Tk2dDragObserver observer)
	{
		var otherSlot = observer.transform.parent;
		var currentChild = slot.transform.GetChild(0);

		currentChild.SetParent(otherSlot, false);
		currentChild.transform.localPosition = new Vector3(0.0f, 0.0f, -0.1f);
		observer.transform.SetParent(slot.transform);
		observer.transform.localPosition = new Vector3(0.0f, 0.0f, -0.1f);
	}
}
