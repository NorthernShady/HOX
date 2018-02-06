using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FilledLineSegmentDraw : BaseSegment {

	[SerializeField, Range(1.0f,1000.0f)]
	public float lineLength = 200.0f;
	[SerializeField]
	public float lineWidthStart = 0.0f;
	[SerializeField]
	public float lineWidthEnd = 30.0f;

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

		Vector2 pointLineStart = Vector2.zero;
		Vector2 pointLineStart1 = pointLineStart + new Vector2(0.0f, lineWidthStart/2.0f);
		Vector2 pointLineStart2 = pointLineStart - new Vector2(0.0f, lineWidthStart/2.0f);

		Vector2 pointLineEnd = pointLineStart + new Vector2 (lineLength,0.0f);
		Vector2 pointLineEnd1 = pointLineEnd + new Vector2(0.0f, lineWidthEnd/2.0f);
		Vector2 pointLineEnd2 = pointLineEnd - new Vector2(0.0f, lineWidthEnd/2.0f);
		points.Add (pointLineStart1);
		points.Add (pointLineEnd1);
		points.Add (pointLineEnd2);
		points.Add (pointLineStart2);

//		//curve
//		float radiusCurve1 = radius1;
//		float radiusCurve2 = radius2;
//
//		Vector2 curve1Point1 = points.Last ();
//		Vector2 curve1Point2 = MathHelper.getPointPositionOnCircle (Vector2.zero, radius2, startAngle+90.0f + (vertexes -1) * step);
//
//		Vector2 tangentCurve1A = MathHelper.getPointPositionOnCircle (Vector2.zero, radiusCurve1, startAngle + 90.0f + endAngle + cubicCurveOffsetAngle);
//		Vector2 tangentCurve1B = MathHelper.getPointPositionOnCircle (Vector2.zero, radiusCurve2, startAngle + 90.0f + endAngle + cubicCurveOffsetAngle);
//
//		float curveStep = 1.0f/cubicCurvePointsCount;
//		for (float i = curveStep; i < 1.0f; i += curveStep) {
//			points.Add (MathHelper.GetPointOnCubicCurve(curve1Point1, curve1Point2, tangentCurve1A, tangentCurve1B, i));
//		}
//		//


		m_shape = new Core.Shape (points);
		var mesh = Core.Utils.ShapeCreator.CreateMesh (m_shape, Color.white, true, 30.0f, 0.1f);
		m_meshFilter.mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
