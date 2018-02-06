using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFreeScrollableAreaEditor : MonoBehaviour {

	[SerializeField]
	Color m_collor = new Color(1,0,0,0.2f); 

	[SerializeField]
	Collider2D m_collider;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
//		if (m_collider) {
//			var bounds = m_collider.bounds;
//			Gizmos.color = m_collor;
//			Gizmos.DrawCube (m_collider.gameObject.transform.position, new Vector3 (bounds.size.x, bounds.size.y, 0.001f));
//		}
	}
}
