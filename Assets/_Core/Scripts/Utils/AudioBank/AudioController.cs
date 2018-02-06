using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AudioController
{
    public AudioBank AudioBank { private get; set; }

	protected struct SoundData {

		public SoundData(AudioSource audioSource = null, SoundEvents.Group soundGroup = SoundEvents.Group.SOUND, float volume = 1.0f) : this() {
			this.audioSource = audioSource;
			this.soundGroup = soundGroup;
			this.volume = volume;
		}

		public SoundEvents.Group soundGroup { private set; get; }
		public AudioSource audioSource { set; get; }
		public float volume { set; get; }
	}

	protected Dictionary<int, SoundData> m_activeAudioSources =  new Dictionary<int, SoundData>();

	private Dictionary<SoundEvents.Group, float> m_soundGroupVolume = new Dictionary<SoundEvents.Group, float>() {
		{SoundEvents.Group.SOUND, 1.0f},
		{SoundEvents.Group.MUSIC, 1.0f}
	};

    public AudioController(AudioBank bank)
    {
        AudioBank = bank;
    }

	public void subscribe() {
		EventManager.Instance.RemoveListener<SoundEvents.Play>(PlaySound);
		EventManager.Instance.RemoveListener<SoundEvents.Stop>(StopSound);
		EventManager.Instance.RemoveListener<SoundEvents.UpdateVolume>(UpdateVolume);
		EventManager.Instance.RemoveListener<SoundEvents.UpdateGroupVolume>(UpdateGroupVolume);
		EventManager.Instance.RemoveListener<SoundEvents.Fade>(FadeSound);
		EventManager.Instance.RemoveListener<SoundEvents.ChangePauseState>(ChangePauseState);

		EventManager.Instance.AddListener<SoundEvents.Play>(PlaySound);
		EventManager.Instance.AddListener<SoundEvents.Stop>(StopSound);
		EventManager.Instance.AddListener<SoundEvents.UpdateVolume>(UpdateVolume);
		EventManager.Instance.AddListener<SoundEvents.UpdateGroupVolume>(UpdateGroupVolume);
		EventManager.Instance.AddListener<SoundEvents.Fade>(FadeSound);
		EventManager.Instance.AddListener<SoundEvents.ChangePauseState>(ChangePauseState);
	}

	public void unsubscribe() {
		EventManager.Instance.RemoveListener<SoundEvents.Play>(PlaySound);
		EventManager.Instance.RemoveListener<SoundEvents.Stop>(StopSound);
		EventManager.Instance.RemoveListener<SoundEvents.UpdateVolume>(UpdateVolume);
		EventManager.Instance.RemoveListener<SoundEvents.UpdateGroupVolume>(UpdateGroupVolume);
		EventManager.Instance.RemoveListener<SoundEvents.Fade>(FadeSound);
		EventManager.Instance.RemoveListener<SoundEvents.ChangePauseState>(ChangePauseState);
	}

    private AudioItem GetAudioItem(AudioIDs audioID)
    {
        List<AudioItem> audioItems = AudioBank[audioID];
        if (audioItems != null && audioItems.Count > 0)
        {
            AudioItem aItem = audioItems[UnityEngine.Random.Range(0, audioItems.Count)];
            return aItem;
        }
        else return null;
    }


	void onAudioObjDestroyed(int audioSourceItemID)
	{
		if (m_activeAudioSources.ContainsKey (audioSourceItemID)) {
			m_activeAudioSources.Remove (audioSourceItemID);
		}
	}

	private void PlaySound(SoundEvents.Play e)
	{
		AudioItem aItem = GetAudioItem(e.audioID);
		if (aItem != null)
		{
			AudioClip clip = aItem.clip;

			if (clip)
			{
				if (e.audioSourceItemID != SoundEvents.SoundEvent.UNDEFINED) {
					if (m_activeAudioSources.ContainsKey (e.audioSourceItemID)) {
						if (m_activeAudioSources [e.audioSourceItemID].audioSource.clip == clip)
							return;
					}
				}

				float volume = aItem.volume;
				bool isLoop = e.playType == SoundEvents.Play.PlayType.LOOP;
				if (e.volume != SoundEvents.SoundEvent.UNDEFINED) {
					volume = e.volume;
				}

				var audioSource = SoundUtils.PlayClipAtPoint (clip, Vector3.zero, volume * m_soundGroupVolume[e.soundGroup], aItem.pitch, isLoop);
				GameObject.DontDestroyOnLoad (audioSource.gameObject);
				if (e.audioSourceItemID != SoundEvents.SoundEvent.UNDEFINED) {
					if (m_activeAudioSources.ContainsKey (e.audioSourceItemID)) {
						m_activeAudioSources [e.audioSourceItemID].audioSource.DOKill ();
						var obj = m_activeAudioSources[e.audioSourceItemID].audioSource.gameObject;
						m_activeAudioSources [e.audioSourceItemID].audioSource.gameObject.GetComponent<AudioObjLifeObserver> ().onDestroy -= onAudioObjDestroyed;
						GameObject.Destroy (obj);
						var tempSoundData = m_activeAudioSources [e.audioSourceItemID];
						m_activeAudioSources [e.audioSourceItemID] = new SoundData(audioSource, tempSoundData.soundGroup, volume);
					} else {
						m_activeAudioSources.Add (e.audioSourceItemID, new SoundData(audioSource, e.soundGroup, volume));
					}
					var lifeObserver = audioSource.gameObject.AddComponent<AudioObjLifeObserver> ();
					lifeObserver.audioSourceID = e.audioSourceItemID;
					lifeObserver.onDestroy -= onAudioObjDestroyed;
					lifeObserver.onDestroy += onAudioObjDestroyed;
				}
			}
		}
	}

	private void StopSound(SoundEvents.Stop e)
	{
		if (m_activeAudioSources.ContainsKey (e.audioSourceItemId)) {
			m_activeAudioSources [e.audioSourceItemId].audioSource.DOKill ();
			var obj = m_activeAudioSources[e.audioSourceItemId].audioSource.gameObject;
			m_activeAudioSources [e.audioSourceItemId].audioSource.gameObject.GetComponent<AudioObjLifeObserver> ().onDestroy -= onAudioObjDestroyed;
			GameObject.Destroy (obj);
			m_activeAudioSources.Remove (e.audioSourceItemId);
		}
	}

	private void FadeSound(SoundEvents.Fade e)
	{
		if (m_activeAudioSources.ContainsKey (e.audioSourceItemID)) {
			var audioSource = m_activeAudioSources [e.audioSourceItemID].audioSource;
			audioSource.DOFade (e.endVolume, e.duration).OnComplete(delegate {
				var soundPairData = m_activeAudioSources [e.audioSourceItemID];
				soundPairData.volume = e.endVolume;
				m_activeAudioSources [e.audioSourceItemID] = soundPairData;
				if (e.stopInEnd) {
					StopSound(new SoundEvents.Stop(e.audioSourceItemID));
				}
			});
		}
	}

	private void ChangePauseState(SoundEvents.ChangePauseState e)
	{
		if (m_activeAudioSources.ContainsKey (e.audioSourceItemID)) {
			var audioSource = m_activeAudioSources [e.audioSourceItemID].audioSource;
			if (e.isPaused) {
				audioSource.Pause ();
			} else {
				audioSource.Play ();
			}
		}
	}
		
	private void UpdateVolume(SoundEvents.UpdateVolume e)
	{
		if (m_activeAudioSources.ContainsKey (e.audioSourceItemID)) {
			var soundData = m_activeAudioSources [e.audioSourceItemID];
			var audioSource = soundData.audioSource;
			audioSource.volume = e.volume * m_soundGroupVolume[soundData.soundGroup];;
		}
	}

	private void UpdateGroupVolume(SoundEvents.UpdateGroupVolume e)
	{
		m_soundGroupVolume [e.soundGroup] = e.volume;
		foreach (var soundDataPair in m_activeAudioSources) {
			soundDataPair.Value.audioSource.volume = soundDataPair.Value.volume * m_soundGroupVolume[soundDataPair.Value.soundGroup];
		}
	}

}
