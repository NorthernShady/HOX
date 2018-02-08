using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPlayButton : MonoBehaviour {

	void onClick()
	{
		SceneManager.LoadScene(k.Scenes.HERO_PICK);
	}
}
