using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AndroidBackButtonListener : MonoBehaviour {

	public UnityEvent onAndroidBackButtonClick;

	// Use this for initialization
	void Start () {		
	}

	void OnEnable ()
	{
		AndroidBackButtonController.OnAndroidBackButtonCallEvent -= onAndroidBackButtonClicked;
		AndroidBackButtonController.OnAndroidBackButtonCallEvent += onAndroidBackButtonClicked;
	}

	void OnDisable ()
	{
		AndroidBackButtonController.OnAndroidBackButtonCallEvent -= onAndroidBackButtonClicked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onAndroidBackButtonClicked()
	{
		if (onAndroidBackButtonClick != null)
			onAndroidBackButtonClick.Invoke();
	}
}
