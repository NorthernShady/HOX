using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathHelper  {


	public static Vector2 getPointPositionOnCircle (Vector2 center, float radius, float angle)
	{
//		(cos(a + arccos(x/r)), sin(a + arcsin(y/r)))
//		return new Vector2 (Mathf.Cos(angle + Mathf.Acos(center.x/radius)), Mathf.Sin(angle + Mathf.Asin(center.y/radius)));
		return (Vector3)center + Quaternion.AngleAxis(angle, Vector3.forward) * (Vector3.right *radius);
	}

	public static Vector3 GetPointOnCubicCurve(Vector3 startPosition, Vector3 endPosition, Vector3 startTangent, Vector3 endTangent, float time)
	{
		float t = time;
		float u = 1f - t;
		float t2 = t * t;
		float u2 = u * u;
		float u3 = u2 * u;
		float t3 = t2 * t;

		Vector3 result =
			(u3) * startPosition +
			(3f * u2 * t) * startTangent +
			(3f * u * t2) * endTangent +
			(t3) * endPosition;

		return result;
	}

	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(
			Vector3.Dot(n, Vector3.Cross(v1, v2)),
			Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
	}

	public static List<Vector3> getFlyPath(Vector3 startPos, Vector3 endPos, int stepCount = 20)
	{
		startPos.z = -20;
		endPos.z = -20;
		Vector3 bPos = new Vector3 (
			UnityEngine.Random.Range(startPos.x + UnityEngine.Random.Range(-1,1)*UnityEngine.Random.Range(3.5f, 4.5f), (startPos.x + endPos.x) /2.0f),
			UnityEngine.Random.Range(startPos.y + UnityEngine.Random.Range(-1,1)*UnityEngine.Random.Range(3.5f, 4.5f), (startPos.y + endPos.y) /2.0f),
			-20
		);

		Vector3 bPos2 = new Vector3 (
			UnityEngine.Random.Range(startPos.x + UnityEngine.Random.Range(-1,1)*UnityEngine.Random.Range(3.5f, 4.5f), (startPos.x + endPos.x) /2.0f),
			UnityEngine.Random.Range(startPos.y + UnityEngine.Random.Range(-1,1)*UnityEngine.Random.Range(3.5f, 4.5f), (startPos.y + endPos.y) /2.0f),
			-20
		);

		var points = new List<Vector3> ();

		float curveStep = 1.0f/stepCount;
		for (float j = curveStep; j < 1.0f; j += curveStep) {
			points.Add (MathHelper.GetPointOnCubicCurve(startPos, endPos, bPos, bPos2, j));
		}
		points.Add (endPos);
		return points;
	}
}
