using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneBlackoutTransitionOut : MonoBehaviour {

	[SerializeField]
	SpriteRenderer m_spriteRenderer;

	[SerializeField]
	float m_fadeDuration;

	[SerializeField]
	float m_scaleDuration;

	[SerializeField]
	float m_startScale = 2.0f;

	[SerializeField]
	Transform m_scaleTransform;

	[SerializeField]
	Transform m_transformCenter;

	public static bool continueFadeTransition = false;

	IEnumerator startScaleCoroutine() {
		yield return new WaitForEndOfFrame ();
		m_scaleTransform.parent = m_transformCenter;
		m_transformCenter.localScale = Vector3.one * m_startScale;
	}

	void doScale() {
		m_transformCenter.DOScale (1.0f, m_scaleDuration);
	}

	void Start () {
		if (continueFadeTransition) {
			var colorTmp = m_spriteRenderer.color;
			colorTmp.a = 1.0f;
			StartCoroutine (startScaleCoroutine ());
			m_spriteRenderer.color = colorTmp;
			m_spriteRenderer.DOFade (0.0f, m_fadeDuration).OnComplete (() => {
				doScale();
			});
			continueFadeTransition = false;
		}
	}

}
