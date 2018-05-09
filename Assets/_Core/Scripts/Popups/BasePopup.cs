using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour {

	protected GameInputController m_gameInputController = null;

	protected virtual void Awake()
	{
		m_gameInputController = FindObjectOfType<GameInputController>();

		m_gameInputController.allowGameTouches = false;
	}

	public virtual void onClose()
	{
		Destroy(this.gameObject);
		m_gameInputController.allowGameTouches = true;
	}
}
