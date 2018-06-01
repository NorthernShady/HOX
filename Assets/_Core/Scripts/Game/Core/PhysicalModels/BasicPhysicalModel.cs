using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPhysicalModel : MonoBehaviour {

	public System.Action<Collider, GameObject> OnEnterTrigger;
	public System.Action<Collider, GameObject> OnExitTrigger;

	private GameObject m_targetObject = null;

	public GameObject targetObject {
		set {
			m_targetObject = value;
		}
		get {
			return m_targetObject;
		}
	}

	public virtual Vector3 getInfoPosition()
	{
		return new Vector3(0.0f, -1.2f, 0.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		var physicalModel = other.GetComponent<BasicPhysicalModel>();
		if (OnEnterTrigger != null)
			OnEnterTrigger(other, physicalModel != null ? physicalModel.targetObject : other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		var physicalModel = other.GetComponent<BasicPhysicalModel>();
		if (OnExitTrigger != null)
			OnExitTrigger(other, physicalModel != null ? physicalModel.targetObject : other.gameObject);
	}
}
