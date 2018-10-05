using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFeature : MonoBehaviour
{
	protected Character m_character = null;

	public virtual void initialize(Character character)
	{
		m_character = character;
	}

	private void onActivate()
	{
		onFeatureActivated();
	}

	protected virtual void onFeatureActivated()
	{
	}

	protected void enableTouches(bool shouldEnable)
	{
		GetComponent<Collider>().enabled = shouldEnable;
	}
}
