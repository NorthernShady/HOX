using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFeature : MonoBehaviour
{
	protected enum State
	{
		ABSENT,
		LOCKED,
		COUNTDOWN,
		ACTIVE
	}

	protected State m_state = State.ABSENT;
	protected Character m_character = null;

	private void onActivate()
	{
		onFeatureActivated();
	}

	protected virtual void onFeatureActivated()
	{
	}
}
