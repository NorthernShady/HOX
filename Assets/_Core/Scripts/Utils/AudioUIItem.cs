using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUIItem : MonoBehaviour {

	public tk2dUIItem uiItem;

	public AudioIDs downButtonSoundId = AudioIDs.NONE;
	public AudioIDs upButtonSoundId = AudioIDs.NONE;
	public AudioIDs clickButtonSoundId =  AudioIDs.CLICK_BUTTON;
	public AudioIDs releaseButtonSoundId = AudioIDs.NONE;

	void OnEnable()
	{
		if (uiItem == null) {
			uiItem = GetComponent<tk2dUIItem> ();
		}
		
		if (uiItem)
		{
			if (downButtonSoundId != AudioIDs.NONE) { uiItem.OnDown += PlayDownSound; }
			if (upButtonSoundId != AudioIDs.NONE) { uiItem.OnUp += PlayUpSound; }
			if (clickButtonSoundId != AudioIDs.NONE) { uiItem.OnClick += PlayClickSound; }
			if (releaseButtonSoundId != AudioIDs.NONE) { uiItem.OnRelease += PlayReleaseSound; }
		}
	}

	void OnDisable()
	{
		if (uiItem)
		{
			uiItem.OnDown -= PlayDownSound; 
			uiItem.OnUp -= PlayUpSound; 
			uiItem.OnClick -= PlayClickSound; 
			uiItem.OnRelease -= PlayReleaseSound;
		}
	}

	private void PlayDownSound()
	{
		PlaySound(downButtonSoundId);
	}

	private void PlayUpSound()
	{
		PlaySound(upButtonSoundId);
	}

	private void PlayClickSound()
	{
		PlaySound(clickButtonSoundId);
	}

	private void PlayReleaseSound()
	{
		PlaySound(releaseButtonSoundId);
	}
		
	private void PlaySound(AudioIDs audioId)
	{
		EventManager.Instance.Raise (new SoundEvents.Play(audioId));
	}
}
