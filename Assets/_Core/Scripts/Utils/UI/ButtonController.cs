using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour {

	[SerializeField]
	Collider m_collider = null;

	[SerializeField]
	tk2dBaseSprite m_sprite = null;

	[SerializeField]
	Color m_disabledColor = Color.white;

	public bool isEnabled {
		set {
			setEnabled(value);
		}
	}

	void setEnabled(bool isEnabled)
	{
		m_collider.enabled = isEnabled;
		m_sprite.color = isEnabled ? Color.white : m_disabledColor;
	}
}
