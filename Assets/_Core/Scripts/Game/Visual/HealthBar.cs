using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	[SerializeField]
	tk2dUIProgressBar m_progressBar = null;

	[SerializeField]
	Character m_character = null;

	void OnEnable()
	{
		m_character.OnHealthChanged += onHealthChanged;
		m_character.OnPhysicsInitialized += onPhysicsInitialized;
	}

	void OnDisable()
	{
		m_character.OnHealthChanged -= onHealthChanged;
		m_character.OnPhysicsInitialized -= onPhysicsInitialized;
	}

	void Start () {
		onHealthChanged(1.0f);
	}

	void onHealthChanged(float percent)
	{
		if (percent > 1.0f) {
			Debug.LogError("health percent = " + percent);
			percent = Mathf.Clamp(0.0f, 1.0f, percent);
		}

		m_progressBar.Value = percent;
	}

	void onPhysicsInitialized(BasicPhysicalModel physicalModel)
	{
		m_progressBar.transform.localPosition = physicalModel.getInfoPosition();
	}
}
