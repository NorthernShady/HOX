using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tk2dReturnButtonNormalState : MonoBehaviour {

	tk2dUIUpDownButton m_uiUpDownButton = null;

	// Use this for initialization
	void OnEnable () {
		if (m_uiUpDownButton == null) {
			m_uiUpDownButton = GetComponent<tk2dUIUpDownButton> ();
		}
		if (m_uiUpDownButton != null) {
			if (m_uiUpDownButton.upStateGO != null) {
				m_uiUpDownButton.upStateGO.SetActive (true);
			}
			if (m_uiUpDownButton.downStateGO != null) {
				m_uiUpDownButton.downStateGO.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
