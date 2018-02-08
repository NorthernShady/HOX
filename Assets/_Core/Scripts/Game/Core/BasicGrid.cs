using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicGrid : MonoBehaviour {

	[SerializeField]
	protected GridData m_gridData;

	public GridData gridData {
		get {
			return m_gridData;
		}
	}

	public abstract void createGrid(GridData gridData);
}
