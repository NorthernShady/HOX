using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnBackButton : MonoBehaviour {

	[SerializeField]
	int m_sceneNumber = 0;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable ()
	{
		AndroidBackButtonController.OnAndroidBackButtonCallEvent -= onAndroidBackButtonClick;
		AndroidBackButtonController.OnAndroidBackButtonCallEvent += onAndroidBackButtonClick;
	}

	void OnDisable ()
	{
		AndroidBackButtonController.OnAndroidBackButtonCallEvent -= onAndroidBackButtonClick;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onAndroidBackButtonClick()
	{
		SceneManager.LoadScene(m_sceneNumber);
	}
}
