using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AndroidBackButtonController : MonoBehaviour {

	public delegate void OnAndroidBackButtonClick();
	public static event OnAndroidBackButtonClick OnAndroidBackButtonCallEvent;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (Application.platform == RuntimePlatform.Android || Application.isEditor) {
				androidBackButtonClick ();
			}
		}
	}

	void androidBackButtonClick()
	{
		if (OnAndroidBackButtonCallEvent != null) {
			OnAndroidBackButtonCallEvent.GetInvocationList ().ToList ().Last ().DynamicInvoke();
		}
		//Application.Quit (); 
	}
}
