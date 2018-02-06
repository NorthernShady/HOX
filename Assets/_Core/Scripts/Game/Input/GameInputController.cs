using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputController : MonoBehaviour {

	public System.Action<Vector3> OnTap;

	void OnEnable()
	{
		IT_Gesture.onMultiTapE += onMultiTap;
//		IT_Gesture.onSwipeE += onSwipe;
	}

	void OnDisable()
	{
		IT_Gesture.onMultiTapE -= onMultiTap;
//		IT_Gesture.onSwipeE -= onSwipe;
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
}
