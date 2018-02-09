using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInputController : MonoBehaviour {

	public System.Action<Vector3> OnTap;
	public System.Action<Vector2, int> OnDraggingStarted;
	public System.Action<Vector2, int> OnDragging;
	public System.Action<Vector2, int> OnDraggingFinished;

	public bool allowGameTouches = true;

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
		RaycastHit hit;
		if (raycast(tap.pos, out hit, 1 << k.Layers.MAP)) {
			if (OnTap != null)
				OnTap(hit.point);
		}
	}

	void onSwipe(SwipeInfo swipeInfo)
	{
	}

	void onDraggingStarted(DragInfo dragInfo)
	{
		RaycastHit hit;
		if (raycast(dragInfo.pos, out hit, 1 << k.Layers.MAP)) {
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

	bool raycast(Vector2 position, out RaycastHit hit, int layerMask = Physics.DefaultRaycastLayers)
	{
		if (isUiTouch(-1) || !allowGameTouches) {
			hit = new RaycastHit();
			return false;
		}

		Ray ray = Camera.main.ScreenPointToRay(position);
		return Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
	}

	public static bool isUiTouch(int touchId)
	{
		var eventSystem = UnityEngine.EventSystems.EventSystem.current;
		bool isPointerOverGameObject = eventSystem.IsPointerOverGameObject() || eventSystem.IsPointerOverGameObject(touchId);
		if (isPointerOverGameObject && eventSystem.currentSelectedGameObject != null) {
			Button button = eventSystem.currentSelectedGameObject.GetComponent<Button>();
			if (button != null)
				return true;
		}

		return false;
	}
}
