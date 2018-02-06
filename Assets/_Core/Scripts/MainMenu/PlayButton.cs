using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

	void onClick()
	{
		SceneManager.LoadScene(k.Scenes.GAME_SCENE);
	}
}
