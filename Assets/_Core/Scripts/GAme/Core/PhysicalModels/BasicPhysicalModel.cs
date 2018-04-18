using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPhysicalModel : MonoBehaviour {

	public System.Action<Collider> OnEnterTrigger;
	public System.Action<Collider> OnExitTrigger;

	void OnTriggerEnter(Collider other)
	{
		if (OnEnterTrigger != null)
			OnEnterTrigger(other);
	}

	void OnTriggerExit(Collider other)
	{
		if (OnExitTrigger != null)
			OnExitTrigger(other);
	}
}
