using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroPickPlayButton : MonoBehaviour {

	void onClick()
	{
		SceneManager.LoadScene(k.Scenes.GAME_SCENE);

	}
}
