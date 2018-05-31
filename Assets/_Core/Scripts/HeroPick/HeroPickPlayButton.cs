using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroPickPlayButton : MonoBehaviour {

	void onClick()
	{
		if (FindObjectOfType<GameDataProxy>().isBotGame)
			SceneManager.LoadScene(k.Scenes.GAME_SCENE);
		else
			SceneManager.LoadScene(k.Scenes.LOBBY);
	}
}
