using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class tk2dUIFullContentCenterLayout : MonoBehaviour {

	[SerializeField] tk2dUILayoutContainerSizer m_plateLayout;
	[SerializeField] tk2dUICenterLayout m_centerLayout;
	List<tk2dUILayout> m_childs = new List<tk2dUILayout>();


	public void addPlate(tk2dUILayout element)
	{
		m_plateLayout.AddLayout (element, tk2dUILayoutItem.FixedSizeLayoutItem());
		element.transform.position = m_plateLayout.transform.position;
		m_childs.Add (element);
	}

	public void setupToCenter ()
	{
		var bounds = GizmoUtils.GetMaxBounds (gameObject);
		m_plateLayout.SetBounds (bounds.min, bounds.max);
		m_centerLayout.setupCoordCenter();
	}

	public GameObject getPlate()
	{
		return m_plateLayout.gameObject;
	}

	public void clear()
	{
		foreach (tk2dUILayout child in m_childs) {
			if (child.gameObject != m_plateLayout.gameObject) {
				m_plateLayout.RemoveLayout (child);
				Destroy (child.gameObject);
			}
		}
		m_childs.Clear ();
	}

	public void clearAndAddFromChilds()
	{
		clear ();
		foreach (Transform child in transform) {
			var uiChild = GetComponent<tk2dUILayout> ();
			if (uiChild != null) {
				addPlate (uiChild);
			}
		}
	}

	void Update () {
		if (!Application.isPlaying) {
			setupToCenter ();
		}
	}
}
