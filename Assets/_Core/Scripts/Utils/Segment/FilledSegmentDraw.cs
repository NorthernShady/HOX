using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FilledSegmentDraw : BaseSegment {

	[SerializeField, Range(1.0f,1000.0f)]
	public float radius1 = 200.0f;
	[SerializeField, Range(1.0f,1000.0f)]
	public float radius2 = 140.5f;
	[SerializeField]
	public float startAngle = 0.0f;
	[SerializeField]
	public float endAngle = 30.0f;

	[SerializeField, Range(1,1000)]
	public int vertexes = 60;
	[SerializeField, Range(1,1000)]
	public int cubicCurvePointsCount = 20;
	[SerializeField, Range(0.01f,180.0f)]
	public float cubicCurveOffsetAngle = 5.0f;

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

		//curve
		float radiusCurve1 = radius1;
		float radiusCurve2 = radius2;

		Vector2 curve1Point1 = points.Last ();
		Vector2 curve1Point2 = MathHelper.getPointPositionOnCircle (Vector2.zero, radius2, startAngle+90.0f + (vertexes -1) * step);

		Vector2 tangentCurve1A = MathHelper.getPointPositionOnCircle (Vector2.zero, radiusCurve1, startAngle + 90.0f + endAngle + cubicCurveOffsetAngle);
		Vector2 tangentCurve1B = MathHelper.getPointPositionOnCircle (Vector2.zero, radiusCurve2, startAngle + 90.0f + endAngle + cubicCurveOffsetAngle);

		float curveStep = 1.0f/cubicCurvePointsCount;
		for (float i = curveStep; i < 1.0f; i += curveStep) {
			points.Add (MathHelper.GetPointOnCubicCurve(curve1Point1, curve1Point2, tangentCurve1A, tangentCurve1B, i));
		}
		//


		for (int i = vertexes -1; i >= 0; i--) {
			Vector2 point = MathHelper.getPointPositionOnCircle (Vector2.zero, radius2, startAngle+90.0f + i * step);
			points.Add (point);
		}


		//curve
		Vector2 curve2Point1 = points.Last ();
		Vector2 curve2Point2 = MathHelper.getPointPositionOnCircle (Vector2.zero, radius1, startAngle+90.0f);

		Vector2 tangentCurve2A = MathHelper.getPointPositionOnCircle (Vector2.zero, radiusCurve1, startAngle + 90.0f - cubicCurveOffsetAngle);
		Vector2 tangentCurve2B = MathHelper.getPointPositionOnCircle (Vector2.zero, radiusCurve2, startAngle + 90.0f - cubicCurveOffsetAngle);

		for (float i = 1.0f - curveStep; i >= curveStep; i -= curveStep) {
			points.Add (MathHelper.GetPointOnCubicCurve(curve2Point2, curve2Point1, tangentCurve2A, tangentCurve2B, i));
		}
		//



		m_shape = new Core.Shape (points);
		var mesh = Core.Utils.ShapeCreator.CreateMesh (m_shape, Color.white, true, 30.0f, 0.1f);
		m_meshFilter.mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
