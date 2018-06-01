using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour {

	[SerializeField]
	GameObject m_characterInfo = null;

	[SerializeField]
	tk2dUIProgressBar m_progressBar = null;

	[SerializeField]
	tk2dTextMesh m_levelInfo = null;

	[SerializeField]
	Character m_character = null;

	void OnEnable()
	{
		m_character.OnHealthChanged += onHealthChanged;
		m_character.OnCharacterInitialized += onInitialize;
		m_character.OnExpChanged += onExpChanged;
	}

	void OnDisable()
	{
		m_character.OnHealthChanged -= onHealthChanged;
		m_character.OnCharacterInitialized -= onInitialize;
		m_character.OnExpChanged -= onExpChanged;
	}

	void Start () {
		onHealthChanged(1.0f);
	}

	void onInitialize(Character character)
	{
		m_characterInfo.transform.localPosition = character.getPhysicalModel().getInfoPosition();
		onExpChanged(character.totalData.level, 0.0f);
	}

	void onHealthChanged(float percent)
	{
		if (percent > 1.0f) {
			Debug.LogError("health percent = " + percent);
			percent = Mathf.Clamp(0.0f, 1.0f, percent);
		}

		m_progressBar.Value = percent;
	}

	void onExpChanged(int level, float percent)
	{
		m_levelInfo.text = "LVL " + level.ToString();
	}
}
