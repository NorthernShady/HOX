using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tk2dDragObserver : MonoBehaviour {

	public System.Action<Tk2dDragObserver> OnDragFinished;

	[SerializeField]
	float m_zShift = -1.0f;

	Vector3 m_startPosition = Vector3.zero;

	void onDragStarted()
	{
		m_startPosition = transform.localPosition;
		transform.localPosition = MathHelper.zShift(m_startPosition, m_zShift, true);
	}

	void onDragFinished()
	{
		// var position = transform.localPosition;
		// transform.localPosition = MathHelper.zShift(position, -m_zShift, true);

		// if (OnDragFinished != null)
		// 	OnDragFinished(this);

		//transform.localPosition = m_startPosition;

		StartCoroutine(dragFinished());
	}

	IEnumerator dragFinished()
	{
		yield return new WaitForSeconds(0.1f);

		if (OnDragFinished != null)
			OnDragFinished(this);
	}
}
