using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerHolder : MonoBehaviour {

	static AudioController m_audioController =  null;

	void OnEnable () {
		if (m_audioController == null) {
			AudioBank bank = Resources.Load<AudioBank> (k.Resources.AUDIO_BANK);
			if (bank != null) {
				m_audioController = new AudioController (bank);
				m_audioController.subscribe ();
			}

			float musicVolume = PlayerPrefs.GetFloat ("MusicVolume",1);
			EventManager.Instance.Raise(new SoundEvents.UpdateGroupVolume(SoundEvents.Group.MUSIC, musicVolume));
			float soundVolume = PlayerPrefs.GetFloat ("SoundVolume",1);
			EventManager.Instance.Raise(new SoundEvents.UpdateGroupVolume(SoundEvents.Group.SOUND, soundVolume));
		}
	}

//	void OnDisable() {
//		m_audioController.unsubscribe ();
//		m_audioController = null;
//	}
}
