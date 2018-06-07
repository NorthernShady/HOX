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
		//if (OnDragFinished != null)
		//	OnDragFinished(this);


		tk2dUIManager.Instance.OverrideClearAllChildrenPresses(GetComponent<tk2dUIItem>());

		transform.localPosition = m_startPosition;
	}
}
