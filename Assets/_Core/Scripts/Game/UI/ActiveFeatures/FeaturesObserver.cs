using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeaturesObserver : MonoBehaviour
{
	[SerializeField]
	List<ActiveFeature> m_features = null;

	protected virtual void Awake()
	{
		FindObjectOfType<Services>().addService(this);
	}

	public void initialize(Hero hero)
	{
		m_features.ForEach(x => x.initialize(hero));
	}
}
