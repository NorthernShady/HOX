using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterTap : MonoBehaviour {

	[SerializeField]
	tk2dTextMesh m_tapToStartText;

	void OnEnable ()
	{
		IT_Gesture.onMultiTapE -= OnMultiTap;
		IT_Gesture.onMultiTapE += OnMultiTap;
	}

	void OnDisable ()
	{
		IT_Gesture.onMultiTapE -= OnMultiTap;
	}

	void OnMultiTap (Tap tap)
	{
		if (m_tapToStartText != null) {
			m_tapToStartText.text = "Loading...";
			m_tapToStartText.ForceBuild ();
		}
		IT_Gesture.onMultiTapE -= OnMultiTap;
		SceneManager.LoadScene (k.Scenes.GAME_SCENE);
	}
}
