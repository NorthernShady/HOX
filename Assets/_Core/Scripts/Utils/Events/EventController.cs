using UnityEngine;
using System.Collections;

public static class EventController
{

    public enum UIEvents
    {
        NewGameStart = 0
    }

    public enum GameEvents
    {
        NewGameState
    }
		
    public delegate void PlayerDataLoadEvent(bool success);
    public static event PlayerDataLoadEvent OnPlayerDataLoadEvent;

    public delegate void PlaySoundEvent(AudioIDs sound, Vector3 pos);
    public static event PlaySoundEvent OnPlaySoundEvent;

    public delegate void UIEvent(UIEvents eventID);
    public static event UIEvent OnUIEvent;

    public delegate void GameEvent(GameEvents eventID);
    public static event GameEvent OnGameEvent;

    public static void CallPlayerDataLoadEvent(bool success)
    {
        if (OnPlayerDataLoadEvent != null)
        {
            OnPlayerDataLoadEvent(success);
            Debug.Log("OnPlayerDataLoadEvent:" + success);
        }
    }

    public static void CallUIEvent(UIEvents eventID)
    {
        if (OnUIEvent != null)
        {
            OnUIEvent(eventID);
            Debug.Log("EventManager:" + eventID);
        }
    }

    public static void CallGameEvent(GameEvents eventID)
    {
        if (OnGameEvent != null)
        {
            OnGameEvent(eventID);
            Debug.Log("EventManager:" + eventID);
        }
    }

    public static void CallPlaySoundEvent(AudioIDs sound, Vector3 pos)
    {
        if (OnPlaySoundEvent != null)
            OnPlaySoundEvent(sound, pos);
    }
}