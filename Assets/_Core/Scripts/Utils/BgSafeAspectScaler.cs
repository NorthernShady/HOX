using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgSafeAspectScaler : MonoBehaviour {

	void Start () {
		var sprite = GetComponent<tk2dSprite> ();
		var cameraSize = Camera.main.GetComponent<tk2dCamera> ().ScreenExtents.size;
		var spriteSize = sprite.GetBounds ().size;
		var newScale = Mathf.Max(cameraSize.x / spriteSize.x, cameraSize.y / spriteSize.y) * 1.01f;

		transform.localScale = new Vector3(newScale, newScale);
	}
}
