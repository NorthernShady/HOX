using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScreen : MonoBehaviour {
	
	void Start () {
		SceneManager.LoadSceneAsync(k.Scenes.INITIALIZE, LoadSceneMode.Additive);
	}
}
