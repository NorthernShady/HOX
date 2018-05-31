using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBotGame : MonoBehaviour {

	void onClick()
	{
		FindObjectOfType<GameDataProxy>().isBotGame = true;
		SceneManager.LoadScene(k.Scenes.GAME_SCENE);
	}
}
