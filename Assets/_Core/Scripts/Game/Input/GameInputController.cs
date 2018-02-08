using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputController : MonoBehaviour {

	public System.Action<Vector3> OnTap;
	public System.Action<Vector2, int> OnDraggingStarted;
	public System.Action<Vector2, int> OnDragging;
	public System.Action<Vector2, int> OnDraggingFinished;

	void OnEnable()
	{
		IT_Gesture.onMultiTapE += onMultiTap;
//		IT_Gesture.onSwipeE += onSwipe;

		IT_Gesture.onDraggingStartE += onDraggingStarted;
		IT_Gesture.onDraggingE += onDragging;
		IT_Gesture.onDraggingEndE += onDraggingFinished;
	}

	void OnDisable()
	{
		IT_Gesture.onMultiTapE -= onMultiTap;
//		IT_Gesture.onSwipeE -= onSwipe;

		IT_Gesture.onDraggingStartE -= onDraggingStarted;
		IT_Gesture.onDraggingE -= onDragging;
		IT_Gesture.onDraggingEndE -= onDraggingFinished;
	}

	void onTap()
	{
	}

	void onMultiTap(Tap tap)
	{
		Ray ray = Camera.main.ScreenPointToRay(tap.pos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << k.Layers.MAP)) {
			if (OnTap != null)
				OnTap(hit.point);
		}
	}

	void onSwipe(SwipeInfo swipeInfo)
	{
	}

	void onDraggingStarted(DragInfo dragInfo)
	{
		Ray ray = Camera.main.ScreenPointToRay(dragInfo.pos);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << k.Layers.MAP)) {
			if (OnDraggingStarted != null)
				OnDraggingStarted(dragInfo.pos, dragInfo.index);
		}

		// Should we store drag indices?
	}

	void onDragging(DragInfo dragInfo)
	{
		if (OnDragging != null)
			OnDragging(dragInfo.pos, dragInfo.index);
	}

	void onDraggingFinished(DragInfo dragInfo)
	{
		if (OnDraggingFinished != null)
			OnDraggingFinished(dragInfo.pos, dragInfo.index);
	}
}
