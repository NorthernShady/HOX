using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(tk2dCameraAnchor))]
[ExecuteInEditMode]
public class SearchTk2dAnchorCamera : MonoBehaviour {

	void Awake() {
		GetComponent<tk2dCameraAnchor> ().AnchorCamera = FindObjectOfType<tk2dCamera> ().ScreenCamera;
	}
}
