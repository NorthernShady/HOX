using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCameraScaler : MonoBehaviour {

	void Start () {
		var sprite = GetComponent<tk2dSprite> ();
		var cameraSize = Camera.main.GetComponent<tk2dCamera> ().ScreenExtents.size;
		var spriteSize = sprite.GetBounds ().size;
		var newScale = new Vector3 (cameraSize.x / spriteSize.x, cameraSize.y / spriteSize.y) * 1.01f;

		transform.localScale = newScale;
	}
}
