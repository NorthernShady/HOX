using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSegment : MonoBehaviour {

	[SerializeField]
	protected MeshFilter m_meshFilter;

	protected Core.Shape m_shape;
	public Core.Shape shape
	{
		get 
		{
			return m_shape;
		}
	}

	public Core.Shape getShapeWithObjPosition()
	{
		var points = new List<Vector2>(m_shape.vectorPoints);
		for (int i = 0; i < points.Count; i++) {
			var point = points [i];
			point = (Vector2)GizmoUtils.RotatePointAroundPivot (point, Vector3.zero, gameObject.transform.localRotation.eulerAngles);
			point = point + (Vector2)gameObject.transform.localPosition;
//			point = (Vector2)GizmoUtils.RotatePointAroundPivot (point, );
			points [i] = point;
		}
		var shape = new Core.Shape (points);
		return shape;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
