using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFeature : ActiveFeature
{
	protected Item m_item = null;

	protected override void onFeatureActivated()
	{
		m_character?.useItem(m_item);
		m_item = null;
	}
}
