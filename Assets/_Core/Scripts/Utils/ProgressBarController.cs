using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarController : MonoBehaviour {

	[SerializeField]
	private GameObject m_spriteObject;

	public GameObject spriteObject {
		get {
			return m_spriteObject;
		}
		set { m_spriteObject = value; }
	}

	[SerializeField]
	private float m_maxValue = 100.0f;
	public float maxValue {
		get { return m_maxValue; }
		set { setMaxValue(value); }
	}
		
	private float m_value = 0.0f;
	public float value {
		get { 
			float curValue = m_value;
			if (curValue > maxValue)
				curValue = maxValue;
			if (curValue < 0.0f)
				curValue = 0.0f;
			return curValue; 
		}
		set { setValue (value); }
	}

	void setValue(float value) {
		m_value = value;
		updateValue (value);
	}

	protected virtual void updateValue(float value) {
		m_spriteObject.transform.localScale = new Vector2(this.value / this.maxValue, m_spriteObject.transform.localScale.y);
	}

	void setMaxValue(float value) {
		m_maxValue = value;
		updateMaxValue (value);
	}

	protected virtual void updateMaxValue(float value) {

	}
}
