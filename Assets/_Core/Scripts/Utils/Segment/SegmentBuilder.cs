using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBuilder : MonoBehaviour {

	[SerializeField]
	List<BaseSegment> m_addSegments;
	[SerializeField]
	List<BaseSegment> m_subSegments;

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


	public void build()
	{
		m_shape = new Core.Shape ();
		foreach(var shape in m_addSegments)
		{
			m_shape = m_shape + shape.getShapeWithObjPosition ();
		}
		foreach(var shape in m_subSegments)
		{
			shape.getShapeWithObjPosition ();
			m_shape = m_shape - shape.getShapeWithObjPosition ();
		}
		var mesh = Core.Utils.ShapeCreator.CreateMesh (m_shape, Color.white, true, 30.0f, 0.1f);
		m_meshFilter.mesh = mesh;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
