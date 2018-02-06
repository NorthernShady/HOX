using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	void Awake()
	{
		float musicVolume = PlayerPrefs.GetFloat (Preferences.PREF_MUSIC_VOLUME, 1);

		//EventManager.Instance.Raise(new SoundEvents.Fade((int)AudioPlayIds.GAME_SCENE, 0, 0.5f, true));
		EventManager.Instance.Raise(new SoundEvents.Play (AudioIDs.MAIN_MENU, (int)AudioPlayIds.MAIN_MENU, 0.0f, SoundEvents.Play.PlayType.LOOP, SoundEvents.Group.MUSIC));
		EventManager.Instance.Raise(new SoundEvents.Fade ((int)AudioPlayIds.MAIN_MENU,musicVolume, 2.0f ,false));
	}
}
