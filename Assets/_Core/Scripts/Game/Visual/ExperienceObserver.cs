using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceObserver : MonoBehaviour {

	[SerializeField]
	tk2dUIProgressBar m_progressBar = null;

	[SerializeField]
	TMPro.TextMeshPro m_levelText = null;

	[SerializeField]
	tk2dSprite m_icon = null;

	private Character m_character = null;

	void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

	void OnDisable()
	{
		m_character.OnExpChanged -= onExpChanged;
	}

	public void initialize(Character character)
	{
		m_character = character;

		m_character.OnExpChanged += onExpChanged;

		m_icon.SetSprite(((Hero)character).type.AsSprite());
		onExpChanged(m_character.data.level, m_character.expPercent);
	}

	void onExpChanged(int level, float expPercent)
	{
		m_levelText.text = level.ToString();
		m_progressBar.Value = expPercent;
	}
}
