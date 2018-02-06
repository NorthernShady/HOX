using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjLifeObserver : MonoBehaviour {

	public System.Action<int> onDestroy;
	public int audioSourceID { get; set;}

	void OnDestroy () {
		if (onDestroy != null) {
			onDestroy (audioSourceID);
		}
	}
}
