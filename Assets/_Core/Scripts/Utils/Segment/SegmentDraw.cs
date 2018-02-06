using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentDraw : BaseSegment {

	[SerializeField]
	public float radius1 = 200.0f;
	[SerializeField]
	public float radius2 = 140.5f;
	[SerializeField]
	public float startAngle = 0.0f;
	[SerializeField]
	public float endAngle = 30.0f;

	[SerializeField]
	public int vertexes = 60;

	[SerializeField]
	public float lineWidth = 1.0f;

	void OnValidate()
	{
		drawSegment ();
	}

	// Use this for initialization
	void Start () {
		drawSegment ();
	}

	public void drawSegment()
	{
		List<Vector2> points = new List<Vector2> ();

		float step = (endAngle - startAngle) / (float)vertexes;
		for (int i = 0; i < vertexes; i++) {
			Vector2 point = MathHelper.getPointPositionOnCircle (Vector2.zero, radius1, startAngle+90.0f + i * step);
			points.Add (point);
		}

		for (int i = vertexes -1; i >= 0; i--) {
			Vector2 point = MathHelper.getPointPositionOnCircle (Vector2.zero, radius2, startAngle+90.0f + i * step);
			points.Add (point);
		}

		m_shape = new Core.Shape (points).OutlineContour(lineWidth);
		var mesh = Core.Utils.ShapeCreator.CreateMesh (m_shape, Color.white, true);
		m_meshFilter.mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
