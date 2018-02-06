using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUtils {

	public static AudioSource PlayClipAtPoint(AudioClip audioClip, Vector3 pos, float volume, float pitch = 1.0f,  bool isLoop = false)
	{
		GameObject tempGO = new GameObject("TempAudio"); // create the temp object
		tempGO.transform.position = pos; // set its position
		AudioSource tempASource = tempGO.AddComponent<AudioSource>(); // add an audio source
		tempASource.clip = audioClip;
		tempASource.volume = volume;
		tempASource.loop = isLoop;
		tempASource.pitch = pitch;
		// set other aSource properties here, if desired
		tempASource.Play(); // start the sound
		if (!isLoop) {
			GameObject.DontDestroyOnLoad (tempGO);
			GameObject.Destroy (tempGO, audioClip.length + 0.1f); // destroy object after clip duration (this will not account for whether it is set to loop)
		}
		return tempASource; // return the AudioSource reference
	}
}
