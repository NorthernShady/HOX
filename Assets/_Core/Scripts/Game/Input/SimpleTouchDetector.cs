using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTouchDetector : MonoBehaviour {

	bool m_isTouchActive = false;
	
	// Update is called once per frame
	void Update(){

		if (Input.touchCount > 1) {
			m_isTouchActive = false;
		}
		else if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.phase==TouchPhase.Began) {
				m_isTouchActive = true;
				IT_Gesture.OnTouchDown(touch);
			}
			else if (touch.phase==TouchPhase.Ended) {
				m_isTouchActive = false;
				IT_Gesture.OnTouchUp(touch);
			}
			else if (m_isTouchActive)
				IT_Gesture.OnTouch(touch);
		}

		#if UNITY_EDITOR
		else if (Input.touchCount == 0) {
			//#if !(UNITY_ANDROID || UNITY_IPHONE) || UNITY_EDITOR
			if(Input.GetMouseButtonDown(0)) {
				m_isTouchActive = true;
				IT_Gesture.OnMouse1Down(Input.mousePosition);
			}
			else if(Input.GetMouseButtonUp(0)) {
				m_isTouchActive = false;
				IT_Gesture.OnMouse1Up(Input.mousePosition);
			}
			else if (Input.GetMouseButton(0) && m_isTouchActive)
				IT_Gesture.OnMouse1(Input.mousePosition);

			if (Input.GetMouseButtonDown(1) || Input.GetMouseButton(1) || Input.GetMouseButtonUp(1))
				m_isTouchActive = false;
		}
		#endif
	}
}
