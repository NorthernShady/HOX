using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	GameDataProxy m_gameDataProxy = null;

	void Awake()
	{
		float musicVolume = PlayerPrefs.GetFloat (Preferences.PREF_MUSIC_VOLUME, 1);

		//EventManager.Instance.Raise(new SoundEvents.Fade((int)AudioPlayIds.GAME_SCENE, 0, 0.5f, true));
		EventManager.Instance.Raise(new SoundEvents.Play (AudioIDs.MAIN_MENU, (int)AudioPlayIds.MAIN_MENU, 0.0f, SoundEvents.Play.PlayType.LOOP, SoundEvents.Group.MUSIC));
		EventManager.Instance.Raise(new SoundEvents.Fade ((int)AudioPlayIds.MAIN_MENU,musicVolume, 2.0f ,false));

		m_gameDataProxy = FindObjectOfType<GameDataProxy>();
        PhotonNetwork.Disconnect();
	}

	void startBotGame()
	{
		m_gameDataProxy.isBotGame = true;
		m_gameDataProxy.team = 0;
		SceneManager.LoadScene(k.Scenes.HERO_PICK);
	}

	void startMultiplayerGame()
	{
		m_gameDataProxy.isBotGame = false;
		SceneManager.LoadScene(k.Scenes.HERO_PICK);
	}
}
